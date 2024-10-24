using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.MSBuild;
using System.Threading.Tasks;

namespace Meeseeks.Core
{
    public class MeeseeksAgent : IMeeseeksAgent
    {
        private readonly IAgentConfiguration _config;
        private readonly IGitService _gitService;
        private readonly ICodeAnalyzer _codeAnalyzer;
        private readonly IDockerService _dockerService;
        private readonly IAgentCommunicationService _communicationService;

        public string AgentId { get; }
        public AgentStatus Status { get; private set; }

        public MeeseeksAgent(
            IAgentConfiguration config,
            IGitService gitService,
            ICodeAnalyzer codeAnalyzer,
            IDockerService dockerService,
            IAgentCommunicationService communicationService)
        {
            AgentId = Guid.NewGuid().ToString();
            _config = config;
            _gitService = gitService;
            _codeAnalyzer = codeAnalyzer;
            _dockerService = dockerService;
            _communicationService = communicationService;
            Status = AgentStatus.Initialized;
        }

        public async Task<AgentResult> ExecuteTask(TaskDefinition task)
        {
            try
            {
                Status = AgentStatus.Working;
                
                // Clone repository and setup workspace
                var workspace = await InitializeWorkspace(task.RepositoryUrl);
                
                // Analyze task requirements
                var taskContext = await _codeAnalyzer.AnalyzeTask(task, workspace);
                
                // Determine if subtasks are needed
                if (taskContext.RequiresSubtasks)
                {
                    return await HandleComplexTask(taskContext);
                }
                
                // Execute simple task
                return await HandleSimpleTask(taskContext);
            }
            catch (Exception ex)
            {
                Status = AgentStatus.Failed;
                return new AgentResult { Success = false, Error = ex.Message };
            }
        }

        private async Task<MSBuildWorkspace> InitializeWorkspace(string repositoryUrl)
        {
            // Create Docker container for isolation
            var containerId = await _dockerService.CreateContainer(_config.DockerImage);
            
            // Clone repository inside container
            await _gitService.CloneRepository(repositoryUrl, containerId);
            
            // Load solution
            var workspace = MSBuildWorkspace.Create();
            var solutionPath = await _gitService.GetSolutionPath(containerId);
            await workspace.OpenSolutionAsync(solutionPath);
            
            return workspace;
        }

        private async Task<AgentResult> HandleComplexTask(TaskContext context)
        {
            var subTasks = await _codeAnalyzer.DivideIntoSubtasks(context);
            var subAgents = new List<IMeeseeksAgent>();

            foreach (var subTask in subTasks)
            {
                var subAgent = await SpawnSubAgent(subTask);
                subAgents.Add(subAgent);
            }

            var results = await Task.WhenAll(
                subAgents.Select(agent => agent.ExecuteTask(subTask))
            );

            return await MergeResults(results);
        }

        private async Task<AgentResult> HandleSimpleTask(TaskContext context)
        {
            // Generate code modifications
            var codeChanges = await _codeAnalyzer.GenerateCodeChanges(context);
            
            // Validate changes
            var validationResult = await ValidateChanges(codeChanges);
            
            if (!validationResult.IsValid)
            {
                return new AgentResult { Success = false, Error = validationResult.Error };
            }

            // Apply changes
            await ApplyChanges(codeChanges);
            
            // Commit and push changes
            await _gitService.CommitAndPush(codeChanges.Description);

            return new AgentResult { Success = true };
        }

        private async Task<IMeeseeksAgent> SpawnSubAgent(TaskDefinition subTask)
        {
            var subAgentContainer = await _dockerService.CreateContainer(_config.DockerImage);
            
            // Create new agent instance
            var subAgent = new MeeseeksAgent(
                _config,
                _gitService,
                _codeAnalyzer,
                _dockerService,
                _communicationService
            );

            // Register sub-agent for communication
            await _communicationService.RegisterAgent(subAgent);

            return subAgent;
        }
    }
}
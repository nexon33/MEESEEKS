using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MEESEEKS.Interfaces;
using MEESEEKS.Models;

namespace MEESEEKS.Core
{
    public class MeeseeksAgent : IAgent
    {
        private readonly IDockerOperations _dockerOps;
        private readonly IGitOperations _gitOps;
        private readonly ICodeAnalyzer _codeAnalyzer;
        private readonly ICodeGenerator _codeGenerator;
        private readonly ISolutionManager _solutionManager;
        private readonly IAgentCommunication _communication;
        private readonly AgentConfiguration _configuration;
        private string? _containerId;

        public string Id => _configuration.Id;
        public AgentStatus Status { get; private set; }

        public MeeseeksAgent(
            AgentConfiguration configuration,
            IDockerOperations dockerOps,
            IGitOperations gitOps,
            ICodeAnalyzer codeAnalyzer,
            ICodeGenerator codeGenerator,
            ISolutionManager solutionManager,
            IAgentCommunication communication)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _dockerOps = dockerOps ?? throw new ArgumentNullException(nameof(dockerOps));
            _gitOps = gitOps ?? throw new ArgumentNullException(nameof(gitOps));
            _codeAnalyzer = codeAnalyzer ?? throw new ArgumentNullException(nameof(codeAnalyzer));
            _codeGenerator = codeGenerator ?? throw new ArgumentNullException(nameof(codeGenerator));
            _solutionManager = solutionManager ?? throw new ArgumentNullException(nameof(solutionManager));
            _communication = communication ?? throw new ArgumentNullException(nameof(communication));
            Status = AgentStatus.Initializing;
        }

        public async Task InitializeContainerAsync()
        {
            try
            {
                _containerId = await _dockerOps.CreateContainerAsync(_configuration);
                if (string.IsNullOrEmpty(_containerId))
                {
                    throw new AgentInitializationException("Failed to create container", null);
                }

                await _dockerOps.StartContainerAsync(_containerId);
                await _solutionManager.LoadSolutionAsync(_configuration.SolutionPath);
                Status = AgentStatus.Ready;
            }
            catch (Exception ex)
            {
                Status = AgentStatus.Error;
                throw new AgentInitializationException("Failed to initialize agent container", ex);
            }
        }

        public async Task<AgentResult> ExecuteTaskAsync(AgentTask task)
        {
            if (task == null) throw new ArgumentNullException(nameof(task));
            if (task.Parameters == null) throw new ArgumentException("Task parameters cannot be null", nameof(task));

            try
            {
                Status = AgentStatus.Busy;

                return task.Type switch
                {
                    TaskType.CodeAnalysis => await ExecuteCodeAnalysisTask(task),
                    TaskType.CodeGeneration => await ExecuteCodeGenerationTask(task),
                    TaskType.Refactoring => await ExecuteRefactoringTask(task),
                    TaskType.Testing => await ExecuteTestingTask(task),
                    _ => throw new NotSupportedException($"Task type {task.Type} is not supported")
                };
            }
            finally
            {
                Status = AgentStatus.Ready;
            }
        }

        public async Task<IAgent> SpawnNewAgentAsync(AgentConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var newAgent = new MeeseeksAgent(
                configuration,
                _dockerOps,
                _gitOps,
                _codeAnalyzer,
                _codeGenerator,
                _solutionManager,
                _communication);

            await newAgent.InitializeContainerAsync();
            await _communication.RegisterAgentAsync(newAgent);
            return newAgent;
        }

        public async Task ShutdownAsync()
        {
            Status = AgentStatus.ShuttingDown;
            await _communication.UnregisterAgentAsync(Id);
            if (!string.IsNullOrEmpty(_containerId))
            {
                await _dockerOps.StopContainerAsync(_containerId);
            }
        }

        private async Task<AgentResult> ExecuteCodeAnalysisTask(AgentTask task)
        {
            if (!task.Parameters.TryGetValue("codeContent", out var content))
                throw new ArgumentException("Code content is required for analysis task");

            var codeContent = content?.ToString() 
                ?? throw new ArgumentException("Code content cannot be null");

            var analysisResult = await _codeAnalyzer.AnalyzeCodeAsync(codeContent);
            if (analysisResult == null)
                throw new InvalidOperationException("Failed to analyze code");

            var modification = await _codeAnalyzer.GenerateCodeModificationAsync(analysisResult);
            if (modification == null)
                throw new InvalidOperationException("Failed to generate code modification");

            return new AgentResult
            {
                TaskId = task.TaskId,
                Success = true,
                Message = "Code analysis completed successfully",
                Output = new Dictionary<string, object>
                {
                    { "analysis", analysisResult },
                    { "modification", modification }
                },
                Errors = new List<string>() // Initialize Errors property
            };
        }

        private async Task<AgentResult> ExecuteCodeGenerationTask(AgentTask task)
        {
            if (!task.Parameters.TryGetValue("className", out var className))
                throw new ArgumentException("Class name is required for code generation task");

            if (!task.Parameters.TryGetValue("namespace", out var ns))
                throw new ArgumentException("Namespace is required for code generation task");

            if (!task.Parameters.TryGetValue("projectName", out var projectName))
                throw new ArgumentException("Project name is required for code generation task");

            var request = new CodeGenerationRequest
            {
                ClassName = className?.ToString() ?? throw new ArgumentException("Class name cannot be null"),
                Namespace = ns?.ToString() ?? throw new ArgumentException("Namespace cannot be null"),
                Dependencies = task.Parameters.TryGetValue("dependencies", out var deps) && deps is List<string> depsList
                    ? depsList
                    : new List<string>(),
                Interfaces = new List<string>(), // Initialize Interfaces property
                Methods = new List<MethodDefinition>() // Initialize Methods property
            };

            var generatedCode = await _codeGenerator.GenerateCodeAsync(request);
            if (generatedCode == null)
                throw new InvalidOperationException("Failed to generate code");

            var tests = await _codeGenerator.GenerateTestsAsync(generatedCode);
            if (tests == null)
                throw new InvalidOperationException("Failed to generate tests");

            var interfaceDefinition = await _codeGenerator.GenerateInterfaceAsync(generatedCode);
            if (interfaceDefinition == null)
                throw new InvalidOperationException("Failed to generate interface");

            var projectNameStr = projectName?.ToString() 
                ?? throw new ArgumentException("Project name cannot be null");

            await _solutionManager.AddFileToProjectAsync(
                projectNameStr,
                $"{generatedCode.ClassName}.cs",
                generatedCode.Code);

            await _solutionManager.AddFileToProjectAsync(
                $"{projectNameStr}.Tests",
                $"{generatedCode.ClassName}Tests.cs",
                tests.TestCode);

            var interfaceContent = interfaceDefinition.ToString() 
                ?? throw new InvalidOperationException("Failed to generate interface content");

            await _solutionManager.AddFileToProjectAsync(
                projectNameStr,
                $"I{generatedCode.ClassName}.cs",
                interfaceContent);

            return new AgentResult
            {
                TaskId = task.TaskId,
                Success = true,
                Message = "Code generation completed successfully",
                Output = new Dictionary<string, object>
                {
                    { "generatedCode", generatedCode },
                    { "tests", tests },
                    { "interface", interfaceDefinition }
                },
                Errors = new List<string>() // Initialize Errors property
            };
        }

        private async Task<AgentResult> ExecuteRefactoringTask(AgentTask task)
        {
            await Task.Yield(); // Ensure async context
            throw new NotImplementedException("Refactoring task implementation is pending");
        }

        private async Task<AgentResult> ExecuteTestingTask(AgentTask task)
        {
            await Task.Yield(); // Ensure async context
            throw new NotImplementedException("Testing task implementation is pending");
        }
    }

    public class AgentInitializationException : Exception
    {
        public AgentInitializationException(string message, Exception? innerException)
            : base(message, innerException)
        {
        }
    }
}

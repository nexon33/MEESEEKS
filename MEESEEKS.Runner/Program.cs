using MEESEEKS.Core;

var config = new AgentConfiguration();
var gitService = new GitService();
var codeAnalyzer = new RoslynCodeAnalyzer();
var dockerService = new DockerService();
var communicationService = new AgentCommunicationService();

var mainAgent = new MeeseeksAgent(
    config,
    gitService,
    codeAnalyzer,
    dockerService,
    communicationService
);

var task = new TaskDefinition
{
    RepositoryUrl = "https://github.com/your/repo",
    TaskDescription = "Refactor UserService to implement SOLID principles",
    Type = TaskType.Refactoring,
    Parameters = new Dictionary<string, object>
    {
        { "targetClass", "UserService" },
        { "principles", new[] { "SRP", "ISP" } }
    }
};

var result = await mainAgent.ExecuteTask(task);

namespace Meeseeks.Core
{
    public interface IMeeseeksAgent
    {
        string AgentId { get; }
        AgentStatus Status { get; }
        Task<AgentResult> ExecuteTask(TaskDefinition task);
    }

    public interface ICodeAnalyzer
    {
        Task<TaskContext> AnalyzeTask(TaskDefinition task, MSBuildWorkspace workspace);
        Task<IEnumerable<TaskDefinition>> DivideIntoSubtasks(TaskContext context);
        Task<CodeChanges> GenerateCodeChanges(TaskContext context);
        Task<ValidationResult> ValidateChanges(CodeChanges changes);
    }

    public interface IGitService
    {
        Task CloneRepository(string repositoryUrl, string containerId);
        Task<string> GetSolutionPath(string containerId);
        Task CommitAndPush(string commitMessage);
    }

    public interface IDockerService
    {
        Task<string> CreateContainer(string imageTag);
        Task DeleteContainer(string containerId);
        Task ExecuteCommand(string containerId, string command);
    }

    public interface IAgentCommunicationService
    {
        Task RegisterAgent(IMeeseeksAgent agent);
        Task UnregisterAgent(string agentId);
        Task SendMessage(string fromAgentId, string toAgentId, AgentMessage message);
    }
}

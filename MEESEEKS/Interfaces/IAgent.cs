using System.Threading.Tasks;
using MEESEEKS.Models;

namespace MEESEEKS.Interfaces
{
    public interface IAgent
    {
        string Id { get; }
        AgentStatus Status { get; }
        Task<AgentResult> ExecuteTaskAsync(AgentTask task);
        Task<IAgent> SpawnNewAgentAsync(AgentConfiguration configuration);
        Task InitializeContainerAsync();
        Task ShutdownAsync();
    }

    public interface ICodeAnalyzer
    {
        Task<CodeAnalysisResult> AnalyzeCodeAsync(string codeContent);
        Task<CodeModification> GenerateCodeModificationAsync(CodeAnalysisResult analysis);
    }

    public interface IGitOperations
    {
        Task CloneRepositoryAsync(string repoUrl, string targetPath);
        Task CommitChangesAsync(string message);
        Task PushChangesAsync();
        Task PullLatestAsync();
    }

    public interface IDockerOperations
    {
        Task<string> CreateContainerAsync(AgentConfiguration config);
        Task StartContainerAsync(string containerId);
        Task StopContainerAsync(string containerId);
        Task<bool> IsContainerRunningAsync(string containerId);
    }

    public interface IAgentCommunication
    {
        Task BroadcastMessageAsync(AgentMessage message);
        Task<AgentMessage> ReceiveMessageAsync();
        Task RegisterAgentAsync(IAgent agent);
        Task UnregisterAgentAsync(string agentId);
    }

    public interface ICodeGenerator
    {
        Task<GeneratedCode> GenerateCodeAsync(CodeGenerationRequest request);
        Task<UnitTestCode> GenerateTestsAsync(GeneratedCode code);
        Task<InterfaceDefinition> GenerateInterfaceAsync(GeneratedCode code);
    }

    public interface ISolutionManager
    {
        Task LoadSolutionAsync(string solutionPath);
        Task AddProjectAsync(string projectName, string projectType);
        Task AddFileToProjectAsync(string projectName, string filePath, string content);
        Task BuildSolutionAsync();
        Task RunTestsAsync();
    }
}

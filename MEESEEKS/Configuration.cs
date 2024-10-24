namespace Meeseeks.Core
{
    public interface IAgentConfiguration
    {
        string DockerImage { get; }
        string WorkspacePath { get; }
        IDictionary<string, string> EnvironmentVariables { get; }
    }

    public class AgentConfiguration : IAgentConfiguration
    {
        public string DockerImage { get; set; } = "meeseeks-agent:latest";
        public string WorkspacePath { get; set; } = "/workspace";
        public IDictionary<string, string> EnvironmentVariables { get; set; } = new Dictionary<string, string>();
    }
}

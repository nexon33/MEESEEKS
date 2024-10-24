using System;
using System.Collections.Generic;

namespace MEESEEKS.Models.Agent
{
    /// <summary>
    /// Represents the configuration settings for a MEESEEKS agent.
    /// </summary>
    public class AgentConfiguration
    {
        /// <summary>
        /// Gets or sets the unique identifier for this agent configuration.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the name of the agent.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the agent's purpose.
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// Gets or sets the Docker container configuration for the agent.
        /// </summary>
        public required Docker.DockerContainerConfig ContainerConfig { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of concurrent tasks this agent can handle.
        /// </summary>
        public int MaxConcurrentTasks { get; set; } = 1;

        /// <summary>
        /// Gets or sets the timeout duration for tasks in seconds.
        /// </summary>
        public int TaskTimeoutSeconds { get; set; } = 300;

        /// <summary>
        /// Gets or sets the capabilities of this agent.
        /// </summary>
        public HashSet<string> Capabilities { get; set; } = new HashSet<string>();

        /// <summary>
        /// Gets or sets additional configuration parameters.
        /// </summary>
        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// Gets or sets the Git repository configuration for code access.
        /// Can be null if the agent doesn't require Git repository access.
        /// </summary>
        public Git.GitRepository? GitRepository { get; set; }

        /// <summary>
        /// Gets or sets the resource limits for the agent.
        /// Can be null to use default container resource limits.
        /// </summary>
        public Docker.ContainerResources? ResourceLimits { get; set; }
    }
}

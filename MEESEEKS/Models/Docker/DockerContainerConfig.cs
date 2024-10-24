using System.Collections.Generic;

namespace MEESEEKS.Models.Docker
{
    /// <summary>
    /// Represents the configuration for a Docker container running a MEESEEKS agent.
    /// </summary>
    public class DockerContainerConfig
    {
        /// <summary>
        /// Unique identifier for the container configuration.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of the container.
        /// </summary>
        public string ContainerName { get; set; }

        /// <summary>
        /// Docker image to use for the container.
        /// </summary>
        public string ImageName { get; set; }

        /// <summary>
        /// Tag of the Docker image.
        /// </summary>
        public string ImageTag { get; set; }

        /// <summary>
        /// Environment variables to set in the container.
        /// </summary>
        public Dictionary<string, string> EnvironmentVariables { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Volume mappings for the container.
        /// </summary>
        public List<VolumeMapping> Volumes { get; set; } = new List<VolumeMapping>();

        /// <summary>
        /// Network configuration for the container.
        /// </summary>
        public NetworkConfig NetworkConfig { get; set; }

        /// <summary>
        /// Resource limits for the container.
        /// </summary>
        public ContainerResources Resources { get; set; }

        /// <summary>
        /// Working directory inside the container.
        /// </summary>
        public string WorkingDirectory { get; set; }

        /// <summary>
        /// Command to run when the container starts.
        /// </summary>
        public string EntryPoint { get; set; }

        /// <summary>
        /// Arguments for the entry point command.
        /// </summary>
        public List<string> CommandArguments { get; set; } = new List<string>();

        /// <summary>
        /// Restart policy for the container.
        /// </summary>
        public RestartPolicy RestartPolicy { get; set; }

        /// <summary>
        /// Health check configuration for the container.
        /// </summary>
        public HealthCheckConfig HealthCheck { get; set; }
    }
}

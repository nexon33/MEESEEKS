using System.Collections.Generic;

namespace MEESEEKS.Models.Docker
{
    /// <summary>
    /// Represents the configuration for a Docker container running a MEESEEKS agent.
    /// This class defines all necessary settings for container creation and management.
    /// </summary>
    public class DockerContainerConfig
    {
        /// <summary>
        /// Unique identifier for the container configuration.
        /// This ID is used to track and manage the container throughout its lifecycle.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Name of the container.
        /// This name must be unique across all containers running on the Docker host.
        /// </summary>
        public required string ContainerName { get; set; }

        /// <summary>
        /// Docker image to use for the container.
        /// Specifies the base image that will be used to create the container.
        /// </summary>
        public required string ImageName { get; set; }

        /// <summary>
        /// Tag of the Docker image.
        /// Specifies the version or variant of the Docker image to use.
        /// </summary>
        public required string ImageTag { get; set; }

        /// <summary>
        /// Environment variables to set in the container.
        /// Key-value pairs of environment variables that will be available inside the container.
        /// </summary>
        public Dictionary<string, string> EnvironmentVariables { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Volume mappings for the container.
        /// Defines how host directories are mounted into the container for persistent storage.
        /// </summary>
        public List<VolumeMapping> Volumes { get; set; } = new List<VolumeMapping>();

        /// <summary>
        /// Network configuration for the container.
        /// Specifies how the container connects to Docker networks and other containers.
        /// </summary>
        public required NetworkConfig NetworkConfig { get; set; }

        /// <summary>
        /// Resource limits for the container.
        /// Defines CPU, memory, and other resource constraints for the container.
        /// </summary>
        public required ContainerResources Resources { get; set; }

        /// <summary>
        /// Working directory inside the container.
        /// The directory where commands will be executed by default inside the container.
        /// </summary>
        public required string WorkingDirectory { get; set; }

        /// <summary>
        /// Command to run when the container starts.
        /// The primary process that will be executed when the container is launched.
        /// </summary>
        public required string EntryPoint { get; set; }

        /// <summary>
        /// Arguments for the entry point command.
        /// Additional parameters passed to the entry point command.
        /// </summary>
        public List<string> CommandArguments { get; set; } = new List<string>();

        /// <summary>
        /// Restart policy for the container.
        /// Defines how the container should behave when it exits or the Docker daemon restarts.
        /// </summary>
        public required RestartPolicy RestartPolicy { get; set; }

        /// <summary>
        /// Health check configuration for the container.
        /// Defines how Docker should check if the container is healthy and functioning properly.
        /// </summary>
        public required HealthCheckConfig HealthCheck { get; set; }
    }
}

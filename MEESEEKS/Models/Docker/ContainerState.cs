using System;

namespace MEESEEKS.Models.Docker
{
    /// <summary>
    /// Represents the current state of a Docker container.
    /// </summary>
    public class ContainerState
    {
        /// <summary>
        /// Whether the container is currently running.
        /// </summary>
        public bool Running { get; set; }

        /// <summary>
        /// Exit code if the container has stopped.
        /// </summary>
        public int? ExitCode { get; set; }

        /// <summary>
        /// Time when the container was started.
        /// </summary>
        public DateTime? StartedAt { get; set; }

        /// <summary>
        /// Time when the container finished.
        /// </summary>
        public DateTime? FinishedAt { get; set; }

        /// <summary>
        /// Current health status of the container.
        /// </summary>
        public string HealthStatus { get; set; }

        /// <summary>
        /// Error message if the container failed to start or encountered an error.
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Process ID of the container's main process.
        /// </summary>
        public int Pid { get; set; }
    }
}

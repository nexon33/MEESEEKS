using System;
using System.Collections.Generic;

namespace MEESEEKS.Models.Docker
{
    /// <summary>
    /// Represents health check configuration for a container.
    /// </summary>
    public class HealthCheckConfig
    {
        /// <summary>
        /// Command to run for health check.
        /// </summary>
        public List<string> Test { get; set; } = new List<string>();

        /// <summary>
        /// Time between health checks in seconds.
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// Time to wait for a health check to complete in seconds.
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// Number of consecutive failures needed to consider unhealthy.
        /// </summary>
        public int Retries { get; set; }

        /// <summary>
        /// Time to wait before first health check in seconds.
        /// </summary>
        public int StartPeriod { get; set; }

        /// <summary>
        /// Whether to disable the health check.
        /// </summary>
        public bool Disabled { get; set; }
    }
}

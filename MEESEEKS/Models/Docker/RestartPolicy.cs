namespace MEESEEKS.Models.Docker
{
    /// <summary>
    /// Represents the restart policy for a container.
    /// </summary>
    public class RestartPolicy
    {
        /// <summary>
        /// Type of restart policy (e.g., "no", "always", "on-failure", "unless-stopped").
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Maximum number of restart attempts.
        /// </summary>
        public int MaximumRetryCount { get; set; }
    }
}

namespace MEESEEKS.Models.Docker
{
    /// <summary>
    /// Represents I/O limits for a container.
    /// </summary>
    public class IoLimits
    {
        /// <summary>
        /// Weight for I/O operations (relative to other containers).
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// Maximum I/O operations per second.
        /// </summary>
        public int MaxIops { get; set; }

        /// <summary>
        /// Maximum I/O bandwidth in bytes per second.
        /// </summary>
        public long MaxBandwidth { get; set; }
    }
}

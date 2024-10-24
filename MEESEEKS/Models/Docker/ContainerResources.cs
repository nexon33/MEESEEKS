namespace MEESEEKS.Models.Docker
{
    /// <summary>
    /// Represents resource limits and reservations for a container.
    /// </summary>
    public class ContainerResources
    {
        /// <summary>
        /// CPU limit (e.g., "1.5" for 1.5 cores).
        /// </summary>
        public double CpuLimit { get; set; }

        /// <summary>
        /// Memory limit in megabytes.
        /// </summary>
        public int MemoryLimitMB { get; set; }

        /// <summary>
        /// CPU reservation (minimum guaranteed CPU).
        /// </summary>
        public double CpuReservation { get; set; }

        /// <summary>
        /// Memory reservation in megabytes.
        /// </summary>
        public int MemoryReservationMB { get; set; }

        /// <summary>
        /// Maximum number of PIDs allowed in the container.
        /// </summary>
        public int PidsLimit { get; set; }

        /// <summary>
        /// IO limits for the container.
        /// </summary>
        public IoLimits IoLimits { get; set; }
    }
}

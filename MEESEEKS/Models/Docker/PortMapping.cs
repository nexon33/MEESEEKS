namespace MEESEEKS.Models.Docker
{
    /// <summary>
    /// Represents a port mapping between host and container.
    /// </summary>
    public class PortMapping
    {
        /// <summary>
        /// Port number on the host machine.
        /// </summary>
        public int HostPort { get; set; }

        /// <summary>
        /// Port number inside the container.
        /// </summary>
        public int ContainerPort { get; set; }

        /// <summary>
        /// Protocol for the port mapping (e.g., tcp, udp).
        /// </summary>
        public string Protocol { get; set; }
    }
}

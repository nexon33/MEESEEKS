using System.Collections.Generic;

namespace MEESEEKS.Models.Docker
{
    /// <summary>
    /// Represents network configuration for a container.
    /// </summary>
    public class NetworkConfig
    {
        /// <summary>
        /// Name of the Docker network to connect to.
        /// </summary>
        public string NetworkName { get; set; } = string.Empty;

        /// <summary>
        /// Whether networking is enabled for the container.
        /// </summary>
        public bool EnableNetworking { get; set; }

        /// <summary>
        /// Network aliases for the container.
        /// </summary>
        public List<string> Aliases { get; set; } = new List<string>();

        /// <summary>
        /// Port mappings between host and container.
        /// </summary>
        public List<PortMapping> PortMappings { get; set; } = new List<PortMapping>();

        /// <summary>
        /// DNS settings for the container.
        /// </summary>
        public DnsConfig? DnsConfig { get; set; }
    }
}

using System.Collections.Generic;

namespace MEESEEKS.Models.Docker
{
    /// <summary>
    /// Represents DNS configuration for a container.
    /// </summary>
    public class DnsConfig
    {
        /// <summary>
        /// List of DNS servers.
        /// </summary>
        public List<string> Nameservers { get; set; } = new List<string>();

        /// <summary>
        /// DNS search domains.
        /// </summary>
        public List<string> SearchDomains { get; set; } = new List<string>();

        /// <summary>
        /// DNS options.
        /// </summary>
        public List<string> Options { get; set; } = new List<string>();
    }
}

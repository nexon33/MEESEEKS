using System.Collections.Generic;

namespace MEESEEKS.Models.Documentation
{
    /// <summary>
    /// Represents documentation for a method's return value.
    /// </summary>
    public class ReturnDocumentation
    {
        /// <summary>
        /// Type of the return value.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Description of the return value.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Possible return values and their meanings.
        /// </summary>
        public Dictionary<string, string> PossibleValues { get; set; } = new Dictionary<string, string>();
    }
}

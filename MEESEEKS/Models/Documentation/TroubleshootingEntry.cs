using System.Collections.Generic;

namespace MEESEEKS.Models.Documentation
{
    /// <summary>
    /// Represents a troubleshooting entry in the documentation.
    /// </summary>
    public class TroubleshootingEntry
    {
        /// <summary>
        /// Description of the issue.
        /// </summary>
        public string Issue { get; set; }

        /// <summary>
        /// Possible causes of the issue.
        /// </summary>
        public List<string> PossibleCauses { get; set; } = new List<string>();

        /// <summary>
        /// Steps to resolve the issue.
        /// </summary>
        public List<string> ResolutionSteps { get; set; } = new List<string>();

        /// <summary>
        /// Prevention tips to avoid the issue.
        /// </summary>
        public List<string> PreventionTips { get; set; } = new List<string>();

        /// <summary>
        /// Related issues or documentation.
        /// </summary>
        public List<string> RelatedIssues { get; set; } = new List<string>();
    }
}

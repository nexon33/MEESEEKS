using System.Collections.Generic;

namespace MEESEEKS.Models.Communication
{
    /// <summary>
    /// Represents filter criteria for a subscription.
    /// </summary>
    public class SubscriptionFilter
    {
        /// <summary>
        /// Minimum severity level of events to receive.
        /// </summary>
        public EventSeverity MinimumSeverity { get; set; }

        /// <summary>
        /// Source patterns to match.
        /// </summary>
        public List<string> SourcePatterns { get; set; } = new List<string>();

        /// <summary>
        /// Tags to match (any of these).
        /// </summary>
        public List<string> Tags { get; set; } = new List<string>();

        /// <summary>
        /// Required tags (all of these must be present).
        /// </summary>
        public List<string> RequiredTags { get; set; } = new List<string>();

        /// <summary>
        /// Excluded tags (none of these should be present).
        /// </summary>
        public List<string> ExcludedTags { get; set; } = new List<string>();

        /// <summary>
        /// Data field conditions that must be met.
        /// </summary>
        public Dictionary<string, string> DataConditions { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Custom filter expression.
        /// </summary>
        public string CustomFilterExpression { get; set; }
    }
}

using System.Collections.Generic;
using MEESEEKS.Models.Communication.Enums;

namespace MEESEEKS.Models.Communication
{
    /// <summary>
    /// Represents filter conditions for events.
    /// </summary>
    public class EventFilter
    {
        /// <summary>
        /// Source patterns to match.
        /// </summary>
        public List<string> SourcePatterns { get; set; } = new List<string>();

        /// <summary>
        /// Tags that must be present.
        /// </summary>
        public List<string> RequiredTags { get; set; } = new List<string>();

        /// <summary>
        /// Data field conditions.
        /// </summary>
        public Dictionary<string, string> DataConditions { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Minimum severity level.
        /// </summary>
        public EventSeverity MinimumSeverity { get; set; }

        /// <summary>
        /// Custom filter expression.
        /// </summary>
        public string? CustomFilterExpression { get; set; }
    }
}

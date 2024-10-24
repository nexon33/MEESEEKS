using System;

namespace MEESEEKS.Models.CodeAnalysis
{
    /// <summary>
    /// Represents a specific change made to code during modification.
    /// </summary>
    public class CodeChange
    {
        /// <summary>
        /// Gets or sets the location where the change was made.
        /// </summary>
        public CodeLocation Location { get; set; } = new CodeLocation();

        /// <summary>
        /// Gets or sets the original code before the change.
        /// </summary>
        public string OriginalCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the modified code after the change.
        /// </summary>
        public string ModifiedCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of why this change was made.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of change (e.g., "Refactor", "Bug Fix", "Performance").
        /// </summary>
        public string ChangeType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the timestamp when this change was made.
        /// </summary>
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets any additional metadata about the change.
        /// </summary>
        public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
    }
}

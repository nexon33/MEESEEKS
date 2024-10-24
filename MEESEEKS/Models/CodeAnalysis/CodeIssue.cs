using System;

namespace MEESEEKS.Models.CodeAnalysis
{
    /// <summary>
    /// Represents an issue found during code analysis.
    /// </summary>
    public class CodeIssue
    {
        /// <summary>
        /// Gets or sets the unique identifier of the issue.
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the issue.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the severity level of the issue.
        /// </summary>
        public IssueSeverity Severity { get; set; }

        /// <summary>
        /// Gets or sets the location where the issue was found.
        /// </summary>
        public CodeLocation Location { get; set; } = new CodeLocation();

        /// <summary>
        /// Gets or sets the suggested fix for the issue.
        /// </summary>
        public string SuggestedFix { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the timestamp when the issue was detected.
        /// </summary>
        public DateTime DetectedAt { get; set; } = DateTime.UtcNow;
    }
}

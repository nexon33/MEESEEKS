namespace MEESEEKS.Models.CodeAnalysis
{
    /// <summary>
    /// Represents a single issue found in the code.
    /// </summary>
    public class CodeIssue
    {
        /// <summary>
        /// Severity level of the issue.
        /// </summary>
        public IssueSeverity Severity { get; set; }

        /// <summary>
        /// Description of the issue.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Location of the issue in the code.
        /// </summary>
        public CodeLocation Location { get; set; }

        /// <summary>
        /// Category of the issue (e.g., "Performance", "Security", "Style").
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Suggested fix for the issue.
        /// </summary>
        public CodeFix SuggestedFix { get; set; }
    }

    /// <summary>
    /// Represents a location in source code.
    /// </summary>
    public class CodeLocation
    {
        /// <summary>
        /// Path to the source file.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Line number in the source file (1-based).
        /// </summary>
        public int LineNumber { get; set; }

        /// <summary>
        /// Column number in the source file (1-based).
        /// </summary>
        public int ColumnNumber { get; set; }

        /// <summary>
        /// Length of the code span.
        /// </summary>
        public int Length { get; set; }
    }

    /// <summary>
    /// Represents a suggested fix for a code issue.
    /// </summary>
    public class CodeFix
    {
        /// <summary>
        /// Description of the fix.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The replacement code.
        /// </summary>
        public string ReplacementCode { get; set; }

        /// <summary>
        /// Location where the fix should be applied.
        /// </summary>
        public CodeLocation FixLocation { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace MEESEEKS.Models.CodeAnalysis
{
    /// <summary>
    /// Represents the result of code analysis performed by Roslyn.
    /// </summary>
    public class CodeAnalysisResult
    {
        /// <summary>
        /// Gets or sets the file path of the analyzed code.
        /// </summary>
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of code issues found during analysis.
        /// </summary>
        public List<CodeIssue> Issues { get; set; } = new List<CodeIssue>();

        /// <summary>
        /// Gets or sets the metrics about the analyzed code.
        /// </summary>
        public CodeMetrics Metrics { get; set; } = new CodeMetrics();

        /// <summary>
        /// Gets or sets the extracted semantic context from the analyzed code.
        /// </summary>
        public CodeContext Context { get; set; } = new CodeContext();

        /// <summary>
        /// Gets or sets the timestamp when the analysis was performed.
        /// </summary>
        public DateTime AnalyzedAt { get; set; } = DateTime.UtcNow;
    }
}

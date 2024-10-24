using System.Collections.Generic;

namespace MEESEEKS.Models.CodeAnalysis
{
    /// <summary>
    /// Represents the result of code analysis performed by Roslyn.
    /// </summary>
    public class CodeAnalysisResult
    {
        /// <summary>
        /// List of code issues found during analysis.
        /// </summary>
        public List<CodeIssue> Issues { get; set; } = new List<CodeIssue>();

        /// <summary>
        /// Metrics about the analyzed code.
        /// </summary>
        public CodeMetrics Metrics { get; set; }

        /// <summary>
        /// Extracted semantic context from the analyzed code.
        /// </summary>
        public CodeContext Context { get; set; }
    }
}

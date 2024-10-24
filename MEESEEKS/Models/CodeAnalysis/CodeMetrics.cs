namespace MEESEEKS.Models.CodeAnalysis
{
    /// <summary>
    /// Represents code metrics collected during analysis.
    /// </summary>
    public class CodeMetrics
    {
        /// <summary>
        /// Cyclomatic complexity of the code.
        /// </summary>
        public int CyclomaticComplexity { get; set; }

        /// <summary>
        /// Lines of code (excluding comments and blank lines).
        /// </summary>
        public int LinesOfCode { get; set; }

        /// <summary>
        /// Number of dependencies.
        /// </summary>
        public int DependencyCount { get; set; }

        /// <summary>
        /// Maintainability index (0-100).
        /// </summary>
        public int MaintainabilityIndex { get; set; }
    }
}

namespace MEESEEKS.Models.CodeAnalysis.Enums
{
    /// <summary>
    /// Severity levels for code issues.
    /// </summary>
    public enum IssueSeverity
    {
        /// <summary>
        /// Information that might be helpful but doesn't indicate a problem.
        /// </summary>
        Info,

        /// <summary>
        /// Potential issues that should be reviewed but aren't critical.
        /// </summary>
        Warning,

        /// <summary>
        /// Serious issues that need to be addressed.
        /// </summary>
        Error
    }
}

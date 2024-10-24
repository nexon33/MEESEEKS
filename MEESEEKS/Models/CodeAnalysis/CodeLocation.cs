namespace MEESEEKS.Models.CodeAnalysis
{
    /// <summary>
    /// Represents a location in source code, including file path and position information.
    /// </summary>
    public class CodeLocation
    {
        /// <summary>
        /// Gets or sets the path to the file containing the code.
        /// </summary>
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the starting line number (1-based).
        /// </summary>
        public int StartLine { get; set; }

        /// <summary>
        /// Gets or sets the ending line number (1-based).
        /// </summary>
        public int EndLine { get; set; }

        /// <summary>
        /// Gets or sets the starting column number (1-based).
        /// </summary>
        public int StartColumn { get; set; }

        /// <summary>
        /// Gets or sets the ending column number (1-based).
        /// </summary>
        public int EndColumn { get; set; }

        /// <summary>
        /// Gets a string representation of the code location.
        /// </summary>
        /// <returns>A string in the format "FilePath:StartLine,StartColumn-EndLine,EndColumn"</returns>
        public override string ToString()
        {
            return $"{FilePath}:{StartLine},{StartColumn}-{EndLine},{EndColumn}";
        }
    }
}

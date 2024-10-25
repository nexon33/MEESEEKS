using MEESEEKS.Models.Git.Enums;

namespace MEESEEKS.Models.Git
{
    /// <summary>
    /// Represents a file change in a Git commit.
    /// </summary>
    public class GitFileChange
    {
        /// <summary>
        /// Path of the changed file.
        /// </summary>
        public required string FilePath { get; set; }

        /// <summary>
        /// Type of change (Added, Modified, Deleted, Renamed).
        /// </summary>
        public GitChangeType ChangeType { get; set; }

        /// <summary>
        /// Number of additions in this file.
        /// </summary>
        public int Additions { get; set; }

        /// <summary>
        /// Number of deletions in this file.
        /// </summary>
        public int Deletions { get; set; }

        /// <summary>
        /// Old file path if the file was renamed.
        /// </summary>
        public string? OldFilePath { get; set; }

        /// <summary>
        /// Mode of the file (e.g., "100644" for regular file).
        /// </summary>
        public required string FileMode { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace MEESEEKS.Models.CodeAnalysis
{
    /// <summary>
    /// Represents a code modification generated from analysis results, including the original content,
    /// modified content, and a detailed list of specific changes made.
    /// </summary>
    public class CodeModification
    {
        /// <summary>
        /// Gets or sets the file path of the code being modified.
        /// This is required to identify which file the modifications apply to.
        /// </summary>
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the original content before modifications.
        /// This is preserved to allow for comparison and potential rollback.
        /// </summary>
        public string OriginalContent { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the modified content after applying changes.
        /// This represents the final state of the code after all modifications.
        /// </summary>
        public string ModifiedContent { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of specific changes made to the code.
        /// Each change includes details about what was modified and why.
        /// </summary>
        public List<CodeChange> Changes { get; set; } = new List<CodeChange>();

        /// <summary>
        /// Gets or sets the timestamp when these modifications were made.
        /// </summary>
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets any additional metadata about the modifications.
        /// </summary>
        public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
    }
}

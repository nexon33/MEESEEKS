using System;
using System.Collections.Generic;

namespace MEESEEKS.Models.Documentation
{
    /// <summary>
    /// Represents a version entry in the documentation.
    /// </summary>
    public class VersionEntry
    {
        /// <summary>
        /// Version number.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Release date of this version.
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Changes included in this version.
        /// </summary>
        public List<string> Changes { get; set; } = new List<string>();

        /// <summary>
        /// Breaking changes in this version.
        /// </summary>
        public List<string> BreakingChanges { get; set; } = new List<string>();

        /// <summary>
        /// Migration instructions for breaking changes.
        /// </summary>
        public string MigrationInstructions { get; set; }
    }
}

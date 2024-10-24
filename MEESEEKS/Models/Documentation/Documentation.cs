using System.Collections.Generic;
using MEESEEKS.Models.CodeGeneration;

namespace MEESEEKS.Models.Documentation
{
    /// <summary>
    /// Represents documentation for generated code.
    /// </summary>
    public class Documentation
    {
        /// <summary>
        /// Overview of the generated code.
        /// </summary>
        public required string Overview { get; set; }

        /// <summary>
        /// Detailed API documentation.
        /// </summary>
        public List<ApiDocumentation> ApiDocs { get; set; } = new List<ApiDocumentation>();

        /// <summary>
        /// Setup and installation instructions.
        /// </summary>
        public required string SetupInstructions { get; set; }

        /// <summary>
        /// Usage examples with explanations.
        /// </summary>
        public List<CodeExample> Examples { get; set; } = new List<CodeExample>();

        /// <summary>
        /// Architecture and design decisions.
        /// </summary>
        public required string ArchitectureOverview { get; set; }

        /// <summary>
        /// Dependencies and their versions.
        /// </summary>
        public List<DependencyInfo> Dependencies { get; set; } = new List<DependencyInfo>();

        /// <summary>
        /// Known limitations and workarounds.
        /// </summary>
        public List<string> Limitations { get; set; } = new List<string>();

        /// <summary>
        /// Version history and changelog.
        /// </summary>
        public List<VersionEntry> VersionHistory { get; set; } = new List<VersionEntry>();

        /// <summary>
        /// Troubleshooting guide.
        /// </summary>
        public List<TroubleshootingEntry> Troubleshooting { get; set; } = new List<TroubleshootingEntry>();
    }
}

using System.Collections.Generic;
using MEESEEKS.Models.CodeGeneration.Enums;

namespace MEESEEKS.Models.CodeGeneration
{
    /// <summary>
    /// Represents a generated source code file.
    /// </summary>
    public class SourceFile
    {
        /// <summary>
        /// Path where the file should be created.
        /// </summary>
        public required string FilePath { get; set; }

        /// <summary>
        /// Content of the file.
        /// </summary>
        public required string Content { get; set; }

        /// <summary>
        /// Programming language of the file.
        /// </summary>
        public required string Language { get; set; }

        /// <summary>
        /// Type of the file (e.g., Class, Interface, Test).
        /// </summary>
        public SourceFileType FileType { get; set; }

        /// <summary>
        /// Dependencies required by this file.
        /// </summary>
        public List<string> Dependencies { get; set; } = new List<string>();

        /// <summary>
        /// Namespace of the code.
        /// </summary>
        public required string Namespace { get; set; }

        /// <summary>
        /// Classes defined in this file.
        /// </summary>
        public List<string> DefinedClasses { get; set; } = new List<string>();

        /// <summary>
        /// Whether this file contains the main entry point.
        /// </summary>
        public bool IsEntryPoint { get; set; }
    }
}

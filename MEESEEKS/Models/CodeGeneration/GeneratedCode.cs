using System;
using System.Collections.Generic;
using MEESEEKS.Models.Documentation;

namespace MEESEEKS.Models.CodeGeneration
{
    /// <summary>
    /// Represents generated code and its associated artifacts.
    /// </summary>
    public class GeneratedCode
    {
        /// <summary>
        /// Collection of generated source files.
        /// </summary>
        public List<SourceFile> SourceFiles { get; set; } = new List<SourceFile>();

        /// <summary>
        /// Collection of generated test files.
        /// </summary>
        public List<SourceFile> TestFiles { get; set; } = new List<SourceFile>();

        /// <summary>
        /// Collection of generated interface files.
        /// </summary>
        public List<SourceFile> InterfaceFiles { get; set; } = new List<SourceFile>();

        /// <summary>
        /// Documentation for the generated code.
        /// </summary>
        public ApiDocumentation Documentation { get; set; }

        /// <summary>
        /// Dependencies required by the generated code.
        /// </summary>
        public List<DependencyInfo> Dependencies { get; set; } = new List<DependencyInfo>();

        /// <summary>
        /// Build or compilation instructions.
        /// </summary>
        public string BuildInstructions { get; set; }

        /// <summary>
        /// Usage examples for the generated code.
        /// </summary>
        public List<CodeExample> Examples { get; set; } = new List<CodeExample>();

        /// <summary>
        /// Known limitations or issues with the generated code.
        /// </summary>
        public List<string> KnownLimitations { get; set; } = new List<string>();

        /// <summary>
        /// Version of the code generator used.
        /// </summary>
        public string GeneratorVersion { get; set; }

        /// <summary>
        /// Timestamp when the code was generated.
        /// </summary>
        public DateTime GeneratedAt { get; set; }
    }
}

using System.Collections.Generic;
using MEESEEKS.Models.CodeAnalysis;

namespace MEESEEKS.Models.CodeGeneration
{
    /// <summary>
    /// Represents a request for code generation.
    /// </summary>
    public class CodeGenerationRequest
    {
        /// <summary>
        /// Description of what code needs to be generated.
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// Programming language to generate code in.
        /// </summary>
        public required string Language { get; set; }

        /// <summary>
        /// Framework or technology stack to use.
        /// </summary>
        public required string Framework { get; set; }

        /// <summary>
        /// Specific requirements or constraints for the generated code.
        /// </summary>
        public List<string> Requirements { get; set; } = new List<string>();

        /// <summary>
        /// Context from existing codebase.
        /// </summary>
        public required CodeContext ExistingContext { get; set; }

        /// <summary>
        /// Whether to generate unit tests.
        /// </summary>
        public bool GenerateTests { get; set; }

        /// <summary>
        /// Whether to generate XML documentation.
        /// </summary>
        public bool GenerateDocumentation { get; set; }

        /// <summary>
        /// Whether to generate interfaces.
        /// </summary>
        public bool GenerateInterfaces { get; set; }

        /// <summary>
        /// Code style preferences.
        /// </summary>
        public required CodeStylePreferences StylePreferences { get; set; }

        /// <summary>
        /// Dependencies to include.
        /// </summary>
        public List<DependencyInfo> Dependencies { get; set; } = new List<DependencyInfo>();
    }
}

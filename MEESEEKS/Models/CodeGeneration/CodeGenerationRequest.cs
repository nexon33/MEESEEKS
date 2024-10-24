using System.Collections.Generic;

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
        public string Description { get; set; }

        /// <summary>
        /// Programming language to generate code in.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Framework or technology stack to use.
        /// </summary>
        public string Framework { get; set; }

        /// <summary>
        /// Specific requirements or constraints for the generated code.
        /// </summary>
        public List<string> Requirements { get; set; } = new List<string>();

        /// <summary>
        /// Context from existing codebase.
        /// </summary>
        public CodeContext ExistingContext { get; set; }

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
        public CodeStylePreferences StylePreferences { get; set; }

        /// <summary>
        /// Dependencies to include.
        /// </summary>
        public List<DependencyInfo> Dependencies { get; set; } = new List<DependencyInfo>();
    }
}

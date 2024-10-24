using System.Collections.Generic;

namespace MEESEEKS.Models.CodeAnalysis
{
    /// <summary>
    /// Represents the semantic context of analyzed code.
    /// </summary>
    public class CodeContext
    {
        /// <summary>
        /// Namespace of the analyzed code.
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// Class or type name.
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Method or member name if applicable.
        /// </summary>
        public string MemberName { get; set; }

        /// <summary>
        /// Dependencies used in the code.
        /// </summary>
        public List<string> Dependencies { get; set; } = new List<string>();

        /// <summary>
        /// References to other types or members.
        /// </summary>
        public List<string> References { get; set; } = new List<string>();
    }
}

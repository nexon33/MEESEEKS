using System.Collections.Generic;

namespace MEESEEKS.Models.Documentation
{
    /// <summary>
    /// Represents API documentation for a code element.
    /// </summary>
    public class ApiDocumentation
    {
        /// <summary>
        /// Name of the documented element.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of the documented element (e.g., class, method).
        /// </summary>
        public string ElementType { get; set; }

        /// <summary>
        /// Description of the element.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Parameters if this is a method.
        /// </summary>
        public List<ParameterDocumentation> Parameters { get; set; } = new List<ParameterDocumentation>();

        /// <summary>
        /// Return value documentation if this is a method.
        /// </summary>
        public ReturnDocumentation ReturnValue { get; set; }

        /// <summary>
        /// Examples of using this API element.
        /// </summary>
        public List<CodeExample> Examples { get; set; } = new List<CodeExample>();

        /// <summary>
        /// Exceptions that can be thrown.
        /// </summary>
        public List<ExceptionDocumentation> Exceptions { get; set; } = new List<ExceptionDocumentation>();

        /// <summary>
        /// Remarks or additional notes.
        /// </summary>
        public string Remarks { get; set; }
    }
}

namespace MEESEEKS.Models.Documentation
{
    /// <summary>
    /// Represents documentation for a method parameter.
    /// </summary>
    public class ParameterDocumentation
    {
        /// <summary>
        /// Name of the parameter.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of the parameter.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Description of the parameter.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Whether the parameter is optional.
        /// </summary>
        public bool IsOptional { get; set; }

        /// <summary>
        /// Default value if the parameter is optional.
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Valid value range or constraints.
        /// </summary>
        public string Constraints { get; set; }
    }
}

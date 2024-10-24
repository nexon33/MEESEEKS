namespace MEESEEKS.Models.CodeGeneration
{
    /// <summary>
    /// Represents information about a code dependency.
    /// </summary>
    public class DependencyInfo
    {
        /// <summary>
        /// Name of the dependency.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Version of the dependency.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Source of the dependency (e.g., NuGet, npm).
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Whether this is a development dependency.
        /// </summary>
        public bool IsDevelopmentDependency { get; set; }

        /// <summary>
        /// License of the dependency.
        /// </summary>
        public string License { get; set; }

        /// <summary>
        /// Whether this is a required dependency.
        /// </summary>
        public bool IsRequired { get; set; }
    }
}

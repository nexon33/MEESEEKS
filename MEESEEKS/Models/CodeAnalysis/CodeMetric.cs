using System;

namespace MEESEEKS.Models.CodeAnalysis
{
    /// <summary>
    /// Represents a metric measurement for code analysis, such as complexity, lines of code, etc.
    /// </summary>
    public class CodeMetric
    {
        /// <summary>
        /// Gets or sets the name of the metric (e.g., "Cyclomatic Complexity", "Lines of Code")
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the numerical value of the metric
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets the unit of measurement (e.g., "paths", "lines", "percentage")
        /// </summary>
        public string Unit { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the timestamp when this metric was collected
        /// </summary>
        public DateTime CollectedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets any additional context or metadata for this metric
        /// </summary>
        public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
    }
}

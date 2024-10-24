using System;
using System.Collections.Generic;

namespace MEESEEKS.Models.CodeAnalysis
{
    /// <summary>
    /// Represents a collection of code metrics with additional metadata and analysis information.
    /// </summary>
    public class CodeMetrics
    {
        /// <summary>
        /// Gets or sets the collection of individual code metrics
        /// </summary>
        public List<CodeMetric> Metrics { get; set; } = new List<CodeMetric>();

        /// <summary>
        /// Gets or sets the timestamp when these metrics were collected
        /// </summary>
        public DateTime CollectedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets any additional metadata about the metrics collection
        /// </summary>
        public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// Implicitly converts a List of CodeMetric to CodeMetrics
        /// </summary>
        /// <param name="metrics">The list of metrics to convert</param>
        public static implicit operator CodeMetrics(List<CodeMetric> metrics)
        {
            return new CodeMetrics { Metrics = metrics };
        }

        /// <summary>
        /// Gets the metrics as a readonly list
        /// </summary>
        /// <returns>An IReadOnlyList of CodeMetric</returns>
        public IReadOnlyList<CodeMetric> AsReadOnly() => Metrics.AsReadOnly();
    }
}

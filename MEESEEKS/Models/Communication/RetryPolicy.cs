using System;
using System.Collections.Generic;

namespace MEESEEKS.Models.Communication
{
    /// <summary>
    /// Represents a retry policy for failed event processing.
    /// </summary>
    public class RetryPolicy
    {
        /// <summary>
        /// Maximum number of retry attempts.
        /// </summary>
        public int MaxRetries { get; set; }

        /// <summary>
        /// Initial delay between retries in milliseconds.
        /// </summary>
        public int InitialDelayMs { get; set; }

        /// <summary>
        /// Maximum delay between retries in milliseconds.
        /// </summary>
        public int MaxDelayMs { get; set; }

        /// <summary>
        /// Factor by which to increase delay after each retry.
        /// </summary>
        public double BackoffMultiplier { get; set; }

        /// <summary>
        /// Types of errors to retry on.
        /// </summary>
        public List<Type> RetryableExceptions { get; set; } = new List<Type>();
    }
}

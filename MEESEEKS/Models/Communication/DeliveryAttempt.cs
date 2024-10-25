using System;
using MEESEEKS.Models.Communication.Enums;

namespace MEESEEKS.Models.Communication
{
    /// <summary>
    /// Represents a single delivery attempt.
    /// </summary>
    public class DeliveryAttempt
    {
        /// <summary>
        /// Time of the attempt.
        /// </summary>
        public DateTime AttemptTime { get; set; }

        /// <summary>
        /// Result of the attempt.
        /// </summary>
        public DeliveryResult Result { get; set; }

        /// <summary>
        /// Duration of the attempt in milliseconds.
        /// </summary>
        public int DurationMs { get; set; }

        /// <summary>
        /// Error information if the attempt failed.
        /// </summary>
        public DeliveryError? Error { get; set; }
    }
}

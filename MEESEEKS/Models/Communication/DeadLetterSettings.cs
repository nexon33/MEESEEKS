using System;

namespace MEESEEKS.Models.Communication
{
    /// <summary>
    /// Settings for dead letter handling.
    /// </summary>
    public class DeadLetterSettings
    {
        /// <summary>
        /// Whether to use dead letter queue.
        /// </summary>
        public bool EnableDeadLetter { get; set; }

        /// <summary>
        /// Maximum attempts before moving to dead letter queue.
        /// </summary>
        public int MaxAttemptsBeforeDeadLetter { get; set; }

        /// <summary>
        /// Time to keep messages in dead letter queue.
        /// </summary>
        public TimeSpan RetentionPeriod { get; set; }

        /// <summary>
        /// Whether to notify when messages are dead lettered.
        /// </summary>
        public bool NotifyOnDeadLetter { get; set; }

        /// <summary>
        /// Queue name for dead letter messages.
        /// </summary>
        public string? DeadLetterQueueName { get; set; }

        /// <summary>
        /// Custom handling logic for dead lettered messages.
        /// </summary>
        public string? CustomHandlerType { get; set; }
    }
}

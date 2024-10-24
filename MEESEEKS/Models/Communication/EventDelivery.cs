using System;
using System.Collections.Generic;

namespace MEESEEKS.Models.Communication
{
    /// <summary>
    /// Represents the delivery status and tracking of an event.
    /// </summary>
    public class EventDelivery
    {
        /// <summary>
        /// Unique identifier for the delivery.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ID of the event being delivered.
        /// </summary>
        public Guid EventId { get; set; }

        /// <summary>
        /// ID of the subscription this delivery is for.
        /// </summary>
        public Guid SubscriptionId { get; set; }

        /// <summary>
        /// Current status of the delivery.
        /// </summary>
        public DeliveryStatus Status { get; set; }

        /// <summary>
        /// Number of delivery attempts made.
        /// </summary>
        public int AttemptCount { get; set; }

        /// <summary>
        /// Time of the last delivery attempt.
        /// </summary>
        public DateTime? LastAttemptTime { get; set; }

        /// <summary>
        /// Time when the delivery was completed.
        /// </summary>
        public DateTime? CompletedTime { get; set; }

        /// <summary>
        /// Next scheduled retry time.
        /// </summary>
        public DateTime? NextRetryTime { get; set; }

        /// <summary>
        /// Error information if delivery failed.
        /// </summary>
        public DeliveryError Error { get; set; }

        /// <summary>
        /// History of delivery attempts.
        /// </summary>
        public List<DeliveryAttempt> AttemptHistory { get; set; } = new List<DeliveryAttempt>();
    }

    /// <summary>
    /// Represents the status of an event delivery.
    /// </summary>
    public enum DeliveryStatus
    {
        /// <summary>
        /// Delivery is pending.
        /// </summary>
        Pending,

        /// <summary>
        /// Delivery is in progress.
        /// </summary>
        InProgress,

        /// <summary>
        /// Delivery completed successfully.
        /// </summary>
        Completed,

        /// <summary>
        /// Delivery failed and will be retried.
        /// </summary>
        RetryPending,

        /// <summary>
        /// Delivery failed permanently.
        /// </summary>
        Failed,

        /// <summary>
        /// Delivery was moved to dead letter queue.
        /// </summary>
        DeadLettered
    }

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
        public DeliveryError Error { get; set; }
    }

    /// <summary>
    /// Represents the result of a delivery attempt.
    /// </summary>
    public enum DeliveryResult
    {
        /// <summary>
        /// Delivery was successful.
        /// </summary>
        Success,

        /// <summary>
        /// Delivery failed but can be retried.
        /// </summary>
        RetryableFailure,

        /// <summary>
        /// Delivery failed permanently.
        /// </summary>
        PermanentFailure,

        /// <summary>
        /// Delivery timed out.
        /// </summary>
        Timeout,

        /// <summary>
        /// Subscriber was not available.
        /// </summary>
        SubscriberUnavailable
    }

    /// <summary>
    /// Represents error information for failed deliveries.
    /// </summary>
    public class DeliveryError
    {
        /// <summary>
        /// Error code.
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Error message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Detailed error information.
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// Stack trace if available.
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        /// Whether this error is retryable.
        /// </summary>
        public bool IsRetryable { get; set; }
    }

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
        public string DeadLetterQueueName { get; set; }

        /// <summary>
        /// Custom handling logic for dead lettered messages.
        /// </summary>
        public string CustomHandlerType { get; set; }
    }
}

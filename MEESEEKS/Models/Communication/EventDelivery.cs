using System;
using System.Collections.Generic;
using MEESEEKS.Models.Communication.Enums;

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
        public DeliveryError? Error { get; set; }

        /// <summary>
        /// History of delivery attempts.
        /// </summary>
        public List<DeliveryAttempt> AttemptHistory { get; set; } = new List<DeliveryAttempt>();
    }
}

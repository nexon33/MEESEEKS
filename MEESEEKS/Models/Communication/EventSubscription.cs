using System;
using System.Collections.Generic;

namespace MEESEEKS.Models.Communication
{
    /// <summary>
    /// Represents a subscription to events.
    /// </summary>
    public class EventSubscription
    {
        /// <summary>
        /// Unique identifier for the subscription.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ID of the subscriber (agent or component).
        /// </summary>
        public Guid SubscriberId { get; set; }

        /// <summary>
        /// Name of the subscription.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Types of events to subscribe to.
        /// </summary>
        public List<EventType> EventTypes { get; set; } = new List<EventType>();

        /// <summary>
        /// Filter criteria for the subscription.
        /// </summary>
        public SubscriptionFilter Filter { get; set; }

        /// <summary>
        /// Delivery options for the subscription.
        /// </summary>
        public DeliveryOptions DeliveryOptions { get; set; }

        /// <summary>
        /// Time when the subscription was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Time when the subscription expires.
        /// </summary>
        public DateTime? ExpiresAt { get; set; }

        /// <summary>
        /// Whether the subscription is currently active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Maximum number of events to receive.
        /// </summary>
        public int? MaxEvents { get; set; }

        /// <summary>
        /// Number of events received so far.
        /// </summary>
        public int EventsReceived { get; set; }
    }
}

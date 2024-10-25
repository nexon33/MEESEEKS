using System;
using System.Collections.Generic;
using MEESEEKS.Models.Communication.Enums;

namespace MEESEEKS.Models.Communication
{
    /// <summary>
    /// Represents a handler for processing agent events.
    /// </summary>
    public class AgentEventHandler
    {
        /// <summary>
        /// Unique identifier for the event handler.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the event handler.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Types of events this handler processes.
        /// </summary>
        public List<EventType> HandledEventTypes { get; set; } = new List<EventType>();

        /// <summary>
        /// Priority of the handler.
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Whether the handler is currently active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Filter conditions for events.
        /// </summary>
        public EventFilter? Filter { get; set; }

        /// <summary>
        /// Maximum number of concurrent events to process.
        /// </summary>
        public int MaxConcurrentEvents { get; set; }

        /// <summary>
        /// Retry policy for failed event processing.
        /// </summary>
        public RetryPolicy? RetryPolicy { get; set; }

        /// <summary>
        /// Error handling strategy.
        /// </summary>
        public ErrorHandlingStrategy ErrorHandling { get; set; }

        /// <summary>
        /// Timeout for event processing in milliseconds.
        /// </summary>
        public int TimeoutMs { get; set; }

        /// <summary>
        /// List of subscriptions to this handler.
        /// </summary>
        public List<EventSubscription> Subscriptions { get; set; } = new List<EventSubscription>();
    }
}

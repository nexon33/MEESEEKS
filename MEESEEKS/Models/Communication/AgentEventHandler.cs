using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public string Name { get; set; }

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
        public EventFilter Filter { get; set; }

        /// <summary>
        /// Maximum number of concurrent events to process.
        /// </summary>
        public int MaxConcurrentEvents { get; set; }

        /// <summary>
        /// Retry policy for failed event processing.
        /// </summary>
        public RetryPolicy RetryPolicy { get; set; }

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

    /// <summary>
    /// Represents filter conditions for events.
    /// </summary>
    public class EventFilter
    {
        /// <summary>
        /// Source patterns to match.
        /// </summary>
        public List<string> SourcePatterns { get; set; } = new List<string>();

        /// <summary>
        /// Tags that must be present.
        /// </summary>
        public List<string> RequiredTags { get; set; } = new List<string>();

        /// <summary>
        /// Data field conditions.
        /// </summary>
        public Dictionary<string, string> DataConditions { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Minimum severity level.
        /// </summary>
        public EventSeverity MinimumSeverity { get; set; }

        /// <summary>
        /// Custom filter expression.
        /// </summary>
        public string CustomFilterExpression { get; set; }
    }

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

    /// <summary>
    /// Represents severity levels for events.
    /// </summary>
    public enum EventSeverity
    {
        /// <summary>
        /// Debug level events.
        /// </summary>
        Debug,

        /// <summary>
        /// Informational events.
        /// </summary>
        Info,

        /// <summary>
        /// Warning level events.
        /// </summary>
        Warning,

        /// <summary>
        /// Error level events.
        /// </summary>
        Error,

        /// <summary>
        /// Critical level events.
        /// </summary>
        Critical
    }

    /// <summary>
    /// Represents error handling strategies.
    /// </summary>
    public enum ErrorHandlingStrategy
    {
        /// <summary>
        /// Continue processing other events on error.
        /// </summary>
        ContinueOnError,

        /// <summary>
        /// Stop processing all events on error.
        /// </summary>
        StopOnError,

        /// <summary>
        /// Retry the failed event according to retry policy.
        /// </summary>
        RetryOnError,

        /// <summary>
        /// Dead letter the failed event.
        /// </summary>
        DeadLetter
    }
}

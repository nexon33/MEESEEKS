using System;
using System.Collections.Generic;

namespace MEESEEKS.Models.Communication
{
    /// <summary>
    /// Represents an event that occurs within the MEESEEKS system.
    /// </summary>
    public class AgentEvent
    {
        /// <summary>
        /// Unique identifier for the event.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Type of the event.
        /// </summary>
        public EventType Type { get; set; }

        /// <summary>
        /// Source of the event (e.g., agent ID, system component).
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Time when the event occurred.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Event data payload.
        /// </summary>
        public Dictionary<string, object> Data { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// Correlation ID for tracking related events.
        /// </summary>
        public Guid CorrelationId { get; set; }

        /// <summary>
        /// Sequence number for ordering events.
        /// </summary>
        public long SequenceNumber { get; set; }

        /// <summary>
        /// Whether the event is system-critical.
        /// </summary>
        public bool IsCritical { get; set; }

        /// <summary>
        /// Categories or tags associated with the event.
        /// </summary>
        public List<string> Tags { get; set; } = new List<string>();
    }

    /// <summary>
    /// Types of events that can occur in the system.
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// Agent lifecycle events (start, stop, pause).
        /// </summary>
        AgentLifecycle,

        /// <summary>
        /// Task-related events.
        /// </summary>
        TaskEvent,

        /// <summary>
        /// Resource-related events.
        /// </summary>
        ResourceEvent,

        /// <summary>
        /// Code generation events.
        /// </summary>
        CodeGeneration,

        /// <summary>
        /// Code analysis events.
        /// </summary>
        CodeAnalysis,

        /// <summary>
        /// Git repository events.
        /// </summary>
        GitEvent,

        /// <summary>
        /// Docker container events.
        /// </summary>
        ContainerEvent,

        /// <summary>
        /// System configuration events.
        /// </summary>
        ConfigurationEvent,

        /// <summary>
        /// Error or exception events.
        /// </summary>
        ErrorEvent,

        /// <summary>
        /// Performance or metrics events.
        /// </summary>
        MetricsEvent
    }
}

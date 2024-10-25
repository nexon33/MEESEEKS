using System;
using System.Collections.Generic;
using MEESEEKS.Models.Communication.Enums;

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
        public required string Source { get; set; }

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
}

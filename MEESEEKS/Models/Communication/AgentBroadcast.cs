using System;
using System.Collections.Generic;

namespace MEESEEKS.Models.Communication
{
    /// <summary>
    /// Represents a broadcast message to multiple agents.
    /// </summary>
    public class AgentBroadcast
    {
        /// <summary>
        /// Unique identifier for the broadcast.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ID of the agent sending the broadcast.
        /// </summary>
        public Guid SenderId { get; set; }

        /// <summary>
        /// Type of the broadcast message.
        /// </summary>
        public BroadcastType Type { get; set; }

        /// <summary>
        /// Content of the broadcast message.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Target agents for the broadcast.
        /// </summary>
        public List<Guid> TargetAgents { get; set; } = new List<Guid>();

        /// <summary>
        /// Additional data associated with the broadcast.
        /// </summary>
        public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// Time when the broadcast was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Time when the broadcast expires.
        /// </summary>
        public DateTime? ExpiresAt { get; set; }

        /// <summary>
        /// Whether acknowledgment is required from recipients.
        /// </summary>
        public bool RequiresAcknowledgment { get; set; }

        /// <summary>
        /// List of agents that have acknowledged the broadcast.
        /// </summary>
        public List<Guid> AcknowledgedBy { get; set; } = new List<Guid>();
    }

    /// <summary>
    /// Types of broadcast messages.
    /// </summary>
    public enum BroadcastType
    {
        /// <summary>
        /// System-wide announcement.
        /// </summary>
        Announcement,

        /// <summary>
        /// Status update broadcast.
        /// </summary>
        StatusUpdate,

        /// <summary>
        /// Resource availability notification.
        /// </summary>
        ResourceNotification,

        /// <summary>
        /// Task coordination broadcast.
        /// </summary>
        TaskCoordination,

        /// <summary>
        /// Emergency notification.
        /// </summary>
        Emergency,

        /// <summary>
        /// System configuration update.
        /// </summary>
        ConfigUpdate
    }
}

using System;
using System.Collections.Generic;

namespace MEESEEKS.Models.Communication
{
    /// <summary>
    /// Represents a message exchanged between MEESEEKS agents.
    /// </summary>
    public class AgentMessage
    {
        /// <summary>
        /// Unique identifier for the message.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ID of the agent sending the message.
        /// </summary>
        public Guid SenderId { get; set; }

        /// <summary>
        /// ID of the agent receiving the message.
        /// </summary>
        public Guid ReceiverId { get; set; }

        /// <summary>
        /// Type of the message.
        /// </summary>
        public MessageType Type { get; set; }

        /// <summary>
        /// Priority level of the message.
        /// </summary>
        public MessagePriority Priority { get; set; }

        /// <summary>
        /// Content of the message.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Additional data associated with the message.
        /// </summary>
        public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// Time when the message was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Time when the message expires.
        /// </summary>
        public DateTime? ExpiresAt { get; set; }

        /// <summary>
        /// Whether the message requires acknowledgment.
        /// </summary>
        public bool RequiresAcknowledgment { get; set; }

        /// <summary>
        /// ID of the conversation this message is part of.
        /// </summary>
        public Guid? ConversationId { get; set; }

        /// <summary>
        /// ID of the message this is in response to.
        /// </summary>
        public Guid? InReplyTo { get; set; }
    }

    /// <summary>
    /// Types of messages that can be exchanged between agents.
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// Task assignment message.
        /// </summary>
        TaskAssignment,

        /// <summary>
        /// Status update message.
        /// </summary>
        StatusUpdate,

        /// <summary>
        /// Request for assistance.
        /// </summary>
        AssistanceRequest,

        /// <summary>
        /// Response to an assistance request.
        /// </summary>
        AssistanceResponse,

        /// <summary>
        /// Error notification.
        /// </summary>
        Error,

        /// <summary>
        /// Task completion notification.
        /// </summary>
        TaskComplete,

        /// <summary>
        /// Resource request.
        /// </summary>
        ResourceRequest,

        /// <summary>
        /// System command.
        /// </summary>
        Command
    }

    /// <summary>
    /// Priority levels for messages.
    /// </summary>
    public enum MessagePriority
    {
        /// <summary>
        /// Low priority message.
        /// </summary>
        Low,

        /// <summary>
        /// Normal priority message.
        /// </summary>
        Normal,

        /// <summary>
        /// High priority message.
        /// </summary>
        High,

        /// <summary>
        /// Critical priority message.
        /// </summary>
        Critical
    }
}

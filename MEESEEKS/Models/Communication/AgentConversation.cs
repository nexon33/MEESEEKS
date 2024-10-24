using System;
using System.Collections.Generic;

namespace MEESEEKS.Models.Communication
{
    /// <summary>
    /// Represents a conversation between MEESEEKS agents.
    /// </summary>
    public class AgentConversation
    {
        /// <summary>
        /// Unique identifier for the conversation.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Topic or subject of the conversation.
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// IDs of agents participating in the conversation.
        /// </summary>
        public List<Guid> Participants { get; set; } = new List<Guid>();

        /// <summary>
        /// Messages exchanged in this conversation.
        /// </summary>
        public List<AgentMessage> Messages { get; set; } = new List<AgentMessage>();

        /// <summary>
        /// Current status of the conversation.
        /// </summary>
        public ConversationStatus Status { get; set; }

        /// <summary>
        /// Time when the conversation started.
        /// </summary>
        public DateTime StartedAt { get; set; }

        /// <summary>
        /// Time when the conversation ended.
        /// </summary>
        public DateTime? EndedAt { get; set; }

        /// <summary>
        /// Task ID associated with this conversation.
        /// </summary>
        public Guid? RelatedTaskId { get; set; }

        /// <summary>
        /// Whether the conversation is private between participants.
        /// </summary>
        public bool IsPrivate { get; set; }
    }

    /// <summary>
    /// Status of a conversation between agents.
    /// </summary>
    public enum ConversationStatus
    {
        /// <summary>
        /// Conversation is active.
        /// </summary>
        Active,

        /// <summary>
        /// Conversation is paused.
        /// </summary>
        Paused,

        /// <summary>
        /// Conversation has completed.
        /// </summary>
        Completed,

        /// <summary>
        /// Conversation was terminated.
        /// </summary>
        Terminated
    }
}

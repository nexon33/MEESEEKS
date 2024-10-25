namespace MEESEEKS.Models.Communication.Enums
{
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

namespace MEESEEKS.Models.Communication.Enums
{
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
}

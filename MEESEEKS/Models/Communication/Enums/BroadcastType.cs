namespace MEESEEKS.Models.Communication.Enums
{
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

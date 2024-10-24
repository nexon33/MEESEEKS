using System;

namespace MEESEEKS.Models.Communication
{
    /// <summary>
    /// Represents a notification sent to or from an agent.
    /// </summary>
    public class AgentNotification
    {
        /// <summary>
        /// Unique identifier for the notification.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Type of the notification.
        /// </summary>
        public NotificationType Type { get; set; }

        /// <summary>
        /// Severity level of the notification.
        /// </summary>
        public NotificationSeverity Severity { get; set; }

        /// <summary>
        /// Title of the notification.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Detailed message of the notification.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Source of the notification.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Time when the notification was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Whether the notification has been read.
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// Whether the notification requires action.
        /// </summary>
        public bool RequiresAction { get; set; }

        /// <summary>
        /// Action URL or command if action is required.
        /// </summary>
        public string ActionTarget { get; set; }
    }

    /// <summary>
    /// Types of notifications.
    /// </summary>
    public enum NotificationType
    {
        /// <summary>
        /// Information notification.
        /// </summary>
        Info,

        /// <summary>
        /// Warning notification.
        /// </summary>
        Warning,

        /// <summary>
        /// Error notification.
        /// </summary>
        Error,

        /// <summary>
        /// Success notification.
        /// </summary>
        Success,

        /// <summary>
        /// Action required notification.
        /// </summary>
        ActionRequired
    }

    /// <summary>
    /// Severity levels for notifications.
    /// </summary>
    public enum NotificationSeverity
    {
        /// <summary>
        /// Low severity.
        /// </summary>
        Low,

        /// <summary>
        /// Medium severity.
        /// </summary>
        Medium,

        /// <summary>
        /// High severity.
        /// </summary>
        High,

        /// <summary>
        /// Critical severity.
        /// </summary>
        Critical
    }
}

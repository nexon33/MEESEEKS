using System;
using MEESEEKS.Models.Communication.Enums;

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
        public required string Title { get; set; }

        /// <summary>
        /// Detailed message of the notification.
        /// </summary>
        public required string Message { get; set; }

        /// <summary>
        /// Source of the notification.
        /// </summary>
        public required string Source { get; set; }

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
        public string? ActionTarget { get; set; }
    }
}

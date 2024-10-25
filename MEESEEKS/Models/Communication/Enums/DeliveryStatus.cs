namespace MEESEEKS.Models.Communication.Enums
{
    /// <summary>
    /// Represents the status of an event delivery.
    /// </summary>
    public enum DeliveryStatus
    {
        /// <summary>
        /// Delivery is pending.
        /// </summary>
        Pending,

        /// <summary>
        /// Delivery is in progress.
        /// </summary>
        InProgress,

        /// <summary>
        /// Delivery completed successfully.
        /// </summary>
        Completed,

        /// <summary>
        /// Delivery failed and will be retried.
        /// </summary>
        RetryPending,

        /// <summary>
        /// Delivery failed permanently.
        /// </summary>
        Failed,

        /// <summary>
        /// Delivery was moved to dead letter queue.
        /// </summary>
        DeadLettered
    }
}

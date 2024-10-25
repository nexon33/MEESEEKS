namespace MEESEEKS.Models.Communication.Enums
{
    /// <summary>
    /// Represents the result of a delivery attempt.
    /// </summary>
    public enum DeliveryResult
    {
        /// <summary>
        /// Delivery was successful.
        /// </summary>
        Success,

        /// <summary>
        /// Delivery failed but can be retried.
        /// </summary>
        RetryableFailure,

        /// <summary>
        /// Delivery failed permanently.
        /// </summary>
        PermanentFailure,

        /// <summary>
        /// Delivery timed out.
        /// </summary>
        Timeout,

        /// <summary>
        /// Subscriber was not available.
        /// </summary>
        SubscriberUnavailable
    }
}

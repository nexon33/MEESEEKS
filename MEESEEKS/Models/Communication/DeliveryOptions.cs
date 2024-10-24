namespace MEESEEKS.Models.Communication
{
    /// <summary>
    /// Represents delivery options for event subscriptions.
    /// </summary>
    public class DeliveryOptions
    {
        /// <summary>
        /// Delivery mode for the events.
        /// </summary>
        public DeliveryMode Mode { get; set; }

        /// <summary>
        /// Maximum number of concurrent deliveries.
        /// </summary>
        public int MaxConcurrentDeliveries { get; set; }

        /// <summary>
        /// Whether to batch events before delivery.
        /// </summary>
        public bool BatchDelivery { get; set; }

        /// <summary>
        /// Maximum batch size if batching is enabled.
        /// </summary>
        public int? MaxBatchSize { get; set; }

        /// <summary>
        /// Maximum time to wait for batch completion in milliseconds.
        /// </summary>
        public int? BatchTimeoutMs { get; set; }

        /// <summary>
        /// Retry policy for failed deliveries.
        /// </summary>
        public RetryPolicy RetryPolicy { get; set; }

        /// <summary>
        /// Dead letter queue settings.
        /// </summary>
        public DeadLetterSettings DeadLetterSettings { get; set; }
    }
}

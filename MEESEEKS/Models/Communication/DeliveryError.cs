namespace MEESEEKS.Models.Communication
{
    /// <summary>
    /// Represents error information for failed deliveries.
    /// </summary>
    public class DeliveryError
    {
        /// <summary>
        /// Error code.
        /// </summary>
        public required string ErrorCode { get; set; }

        /// <summary>
        /// Error message.
        /// </summary>
        public required string Message { get; set; }

        /// <summary>
        /// Detailed error information.
        /// </summary>
        public required string Details { get; set; }

        /// <summary>
        /// Stack trace if available.
        /// </summary>
        public string? StackTrace { get; set; }

        /// <summary>
        /// Whether this error is retryable.
        /// </summary>
        public bool IsRetryable { get; set; }
    }
}

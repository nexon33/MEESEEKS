namespace MEESEEKS.Models.Communication.Enums
{
    /// <summary>
    /// Represents error handling strategies.
    /// </summary>
    public enum ErrorHandlingStrategy
    {
        /// <summary>
        /// Continue processing other events on error.
        /// </summary>
        ContinueOnError,

        /// <summary>
        /// Stop processing all events on error.
        /// </summary>
        StopOnError,

        /// <summary>
        /// Retry the failed event according to retry policy.
        /// </summary>
        RetryOnError,

        /// <summary>
        /// Dead letter the failed event.
        /// </summary>
        DeadLetter
    }
}

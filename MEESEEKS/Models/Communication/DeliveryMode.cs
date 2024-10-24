namespace MEESEEKS.Models.Communication
{
    /// <summary>
    /// Delivery modes for event subscriptions.
    /// </summary>
    public enum DeliveryMode
    {
        /// <summary>
        /// Push events to the subscriber.
        /// </summary>
        Push,

        /// <summary>
        /// Subscriber pulls events.
        /// </summary>
        Pull,

        /// <summary>
        /// Events are streamed to the subscriber.
        /// </summary>
        Stream
    }
}

using MEESEEKS.Models.Communication.Enums;

namespace MEESEEKS.Models.Communication
{
    /// <summary>
    /// Represents delivery mode configuration.
    /// </summary>
    public class DeliveryModeConfig
    {
        /// <summary>
        /// The delivery mode to use.
        /// </summary>
        public DeliveryMode Mode { get; set; }

        /// <summary>
        /// Additional configuration options specific to the delivery mode.
        /// </summary>
        public Dictionary<string, string>? Options { get; set; }
    }
}

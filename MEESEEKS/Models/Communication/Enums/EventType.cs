namespace MEESEEKS.Models.Communication.Enums
{
    /// <summary>
    /// Types of events that can occur in the system.
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// Agent lifecycle events (start, stop, pause).
        /// </summary>
        AgentLifecycle,

        /// <summary>
        /// Task-related events.
        /// </summary>
        TaskEvent,

        /// <summary>
        /// Resource-related events.
        /// </summary>
        ResourceEvent,

        /// <summary>
        /// Code generation events.
        /// </summary>
        CodeGeneration,

        /// <summary>
        /// Code analysis events.
        /// </summary>
        CodeAnalysis,

        /// <summary>
        /// Git repository events.
        /// </summary>
        GitEvent,

        /// <summary>
        /// Docker container events.
        /// </summary>
        ContainerEvent,

        /// <summary>
        /// System configuration events.
        /// </summary>
        ConfigurationEvent,

        /// <summary>
        /// Error or exception events.
        /// </summary>
        ErrorEvent,

        /// <summary>
        /// Performance or metrics events.
        /// </summary>
        MetricsEvent
    }
}

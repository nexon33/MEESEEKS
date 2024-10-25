namespace MEESEEKS.Models.Agent.Enums
{
    /// <summary>
    /// Represents the current status of an agent in the MEESEEKS system.
    /// </summary>
    public enum AgentStatus
    {
        /// <summary>
        /// Agent is initializing and setting up its environment.
        /// </summary>
        Initializing,

        /// <summary>
        /// Agent is ready to accept tasks.
        /// </summary>
        Ready,

        /// <summary>
        /// Agent is currently processing a task.
        /// </summary>
        Working,

        /// <summary>
        /// Agent has completed its task and is preparing to shut down.
        /// </summary>
        Completed,

        /// <summary>
        /// Agent encountered an error during execution.
        /// </summary>
        Error
    }
}

using System;

namespace MEESEEKS.Models.Agent
{
    /// <summary>
    /// Base model for all MEESEEKS agents, providing core identification and status information.
    /// </summary>
    public class AgentModel
    {
        /// <summary>
        /// Unique identifier for the agent.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Human-readable name of the agent.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of the agent, determining its primary responsibility.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Current status of the agent.
        /// </summary>
        public AgentStatus Status { get; set; }

        /// <summary>
        /// ID of the parent agent that spawned this agent, if any.
        /// </summary>
        public Guid? ParentAgentId { get; set; }

        /// <summary>
        /// Docker container ID where this agent is running.
        /// </summary>
        public string ContainerId { get; set; }
    }
}

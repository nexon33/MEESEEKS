using System.Threading.Tasks;
using MEESEEKS.Models.Communication;

namespace MEESEEKS.Interfaces
{
    /// <summary>
    /// Provides functionality for inter-agent communication and message handling.
    /// </summary>
    public interface IAgentCommunication
    {
        /// <summary>
        /// Broadcasts a message to all registered agents.
        /// </summary>
        /// <param name="message">The message to broadcast.</param>
        /// <returns>A task representing the asynchronous broadcast operation.</returns>
        Task BroadcastMessageAsync(AgentMessage message);

        /// <summary>
        /// Receives the next available message for this agent.
        /// </summary>
        /// <returns>A task representing the asynchronous operation that returns the received AgentMessage.</returns>
        Task<AgentMessage> ReceiveMessageAsync();

        /// <summary>
        /// Registers an agent with the communication system.
        /// </summary>
        /// <param name="agent">The agent to register.</param>
        /// <returns>A task representing the asynchronous registration operation.</returns>
        Task RegisterAgentAsync(IAgent agent);

        /// <summary>
        /// Unregisters an agent from the communication system.
        /// </summary>
        /// <param name="agentId">The ID of the agent to unregister.</param>
        /// <returns>A task representing the asynchronous unregistration operation.</returns>
        Task UnregisterAgentAsync(string agentId);
    }
}

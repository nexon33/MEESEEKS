using System.Threading.Tasks;
using MEESEEKS.Models.Agent;
using MEESEEKS.Models.Task;

namespace MEESEEKS.Interfaces
{
    /// <summary>
    /// Represents the core agent interface that defines the fundamental capabilities of a MEESEEKS agent.
    /// </summary>
    public interface IAgent
    {
        /// <summary>
        /// Gets the unique identifier of the agent.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Gets the current status of the agent.
        /// </summary>
        AgentStatus Status { get; }

        /// <summary>
        /// Executes a task asynchronously and returns the result.
        /// </summary>
        /// <param name="task">The task to be executed by the agent.</param>
        /// <returns>A task representing the asynchronous operation that returns an AgentResult containing the outcome of the task execution.</returns>
        Task<AgentResult> ExecuteTaskAsync(AgentTask task);

        /// <summary>
        /// Creates and initializes a new agent instance with the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration settings for the new agent.</param>
        /// <returns>A task representing the asynchronous operation that returns the newly created IAgent instance.</returns>
        Task<IAgent> SpawnNewAgentAsync(AgentConfiguration configuration);

        /// <summary>
        /// Initializes the agent's container environment asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous container initialization operation.</returns>
        Task InitializeContainerAsync();

        /// <summary>
        /// Performs a graceful shutdown of the agent, cleaning up resources and stopping any ongoing operations.
        /// </summary>
        /// <returns>A task representing the asynchronous shutdown operation.</returns>
        Task ShutdownAsync();
    }
}

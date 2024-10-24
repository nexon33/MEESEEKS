using System.Threading.Tasks;
using MEESEEKS.Models.Agent;

namespace MEESEEKS.Interfaces
{
    /// <summary>
    /// Provides functionality for managing Docker container operations.
    /// </summary>
    public interface IDockerOperations
    {
        /// <summary>
        /// Creates a new Docker container with the specified configuration.
        /// </summary>
        /// <param name="config">The configuration settings for the container.</param>
        /// <returns>A task representing the asynchronous operation that returns the container ID as a string.</returns>
        Task<string> CreateContainerAsync(AgentConfiguration config);

        /// <summary>
        /// Starts a Docker container with the specified container ID.
        /// </summary>
        /// <param name="containerId">The ID of the container to start.</param>
        /// <returns>A task representing the asynchronous start operation.</returns>
        Task StartContainerAsync(string containerId);

        /// <summary>
        /// Stops a Docker container with the specified container ID.
        /// </summary>
        /// <param name="containerId">The ID of the container to stop.</param>
        /// <returns>A task representing the asynchronous stop operation.</returns>
        Task StopContainerAsync(string containerId);

        /// <summary>
        /// Checks if a Docker container with the specified ID is currently running.
        /// </summary>
        /// <param name="containerId">The ID of the container to check.</param>
        /// <returns>A task representing the asynchronous operation that returns true if the container is running, false otherwise.</returns>
        Task<bool> IsContainerRunningAsync(string containerId);
    }
}

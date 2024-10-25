using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MEESEEKS.Models.Docker;
using MEESEEKS.Models.Agent;

namespace MEESEEKS.Core
{
    public partial class MeeseeksAgent
    {
        /// <summary>
        /// Initializes the Docker container for this agent.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when container creation or startup fails.</exception>
        public async Task InitializeContainerAsync()
        {
            try
            {
                var containerConfig = new DockerContainerConfig
                {
                    Id = _config.Id.ToString(),
                    ContainerName = $"meeseeks-{_config.Id}",
                    ImageName = "meeseeks/agent",
                    ImageTag = "latest",
                    WorkingDirectory = "/workspace",
                    EntryPoint = "/bin/bash",
                    NetworkConfig = new NetworkConfig 
                    { 
                        DnsConfig = new DnsConfig
                        {
                            Nameservers = new List<string> { "8.8.8.8", "8.8.4.4" }
                        }
                    },
                    Resources = new ContainerResources
                    {
                        CpuLimit = 2.0,
                        MemoryLimit = "2g",
                        IoLimits = new IoLimits
                        {
                            ReadBytesPerSecond = 100_000_000,
                            WriteBytesPerSecond = 100_000_000,
                            MaxBandwidth = 200_000_000
                        }
                    },
                    RestartPolicy = new RestartPolicy
                    {
                        Type = "on-failure",
                        MaximumRetryCount = 3
                    },
                    HealthCheck = new HealthCheckConfig
                    {
                        Test = new List<string> { "CMD", "dotnet", "--info" },
                        Interval = TimeSpan.FromSeconds(30),
                        Timeout = TimeSpan.FromSeconds(10),
                        Retries = 3,
                        StartPeriod = TimeSpan.FromSeconds(5)
                    }
                };

                _containerId = await _dockerOperations.CreateContainerAsync(_config);
                if (string.IsNullOrEmpty(_containerId))
                {
                    throw new InvalidOperationException("Failed to create container");
                }

                await _dockerOperations.StartContainerAsync(_containerId);
                Status = AgentStatus.Ready;
            }
            catch (Exception ex)
            {
                Status = AgentStatus.Error;
                throw new InvalidOperationException($"Failed to initialize container: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Performs a graceful shutdown of the agent, cleaning up resources and stopping the container.
        /// </summary>
        /// <returns>A task representing the asynchronous shutdown operation.</returns>
        public async Task ShutdownAsync()
        {
            if (_containerId != null)
            {
                try
                {
                    var isRunning = await _dockerOperations.IsContainerRunningAsync(_containerId);
                    if (isRunning)
                    {
                        await _dockerOperations.StopContainerAsync(_containerId);
                    }
                }
                catch (Exception ex)
                {
                    // Log the error but don't rethrow as we're shutting down
                    Console.WriteLine($"Error during container shutdown: {ex.Message}");
                }
                finally
                {
                    Status = AgentStatus.Completed;
                    _containerId = null;
                }
            }
        }

        /// <summary>
        /// Disposes of the agent's resources.
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                if (_containerId != null)
                {
                    // Ensure container is stopped during disposal
                    ShutdownAsync().GetAwaiter().GetResult();
                }
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }
    }
}

using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text;
using MEESEEKS.Interfaces;
using MEESEEKS.Models.Agent;
using MEESEEKS.Models.Docker;

namespace MEESEEKS.Core
{
    /// <summary>
    /// Provides functionality for managing Docker container operations.
    /// </summary>
    public class DockerOperations : IDockerOperations
    {
        private readonly string _dockerCommand = "docker";

        /// <summary>
        /// Creates a new Docker container with the specified configuration.
        /// </summary>
        /// <param name="config">The configuration settings for the container.</param>
        /// <returns>A task representing the asynchronous operation that returns the container ID as a string.</returns>
        public async Task<string> CreateContainerAsync(AgentConfiguration config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            if (config.ContainerConfig == null) throw new ArgumentException("Container configuration is required", nameof(config));

            var args = new StringBuilder();
            args.Append("create ");
            
            // Set container name
            args.Append($"--name {config.ContainerConfig.ContainerName} ");

            // Set resource limits
            if (config.ContainerConfig.Resources != null)
            {
                args.Append($"--cpus={config.ContainerConfig.Resources.CpuLimit} ");
                args.Append($"--memory={config.ContainerConfig.Resources.MemoryLimit} ");
            }

            // Mount volumes
            if (config.ContainerConfig.Volumes != null)
            {
                foreach (var volume in config.ContainerConfig.Volumes)
                {
                    args.Append($"-v {volume.HostPath}:{volume.ContainerPath} ");
                }
            }

            // Set working directory
            if (!string.IsNullOrEmpty(config.ContainerConfig.WorkingDirectory))
            {
                args.Append($"-w {config.ContainerConfig.WorkingDirectory} ");
            }

            // Set image
            args.Append($"{config.ContainerConfig.ImageName}:{config.ContainerConfig.ImageTag}");

            var result = await ExecuteDockerCommandAsync(args.ToString());
            return result.Trim();
        }

        /// <summary>
        /// Starts a Docker container with the specified container ID.
        /// </summary>
        /// <param name="containerId">The ID of the container to start.</param>
        /// <returns>A task representing the asynchronous start operation.</returns>
        public async Task StartContainerAsync(string containerId)
        {
            await ExecuteDockerCommandAsync($"start {containerId}");
        }

        /// <summary>
        /// Stops a Docker container with the specified container ID.
        /// </summary>
        /// <param name="containerId">The ID of the container to stop.</param>
        /// <returns>A task representing the asynchronous stop operation.</returns>
        public async Task StopContainerAsync(string containerId)
        {
            await ExecuteDockerCommandAsync($"stop {containerId}");
        }

        /// <summary>
        /// Checks if a Docker container with the specified ID is currently running.
        /// </summary>
        /// <param name="containerId">The ID of the container to check.</param>
        /// <returns>A task representing the asynchronous operation that returns true if the container is running, false otherwise.</returns>
        public async Task<bool> IsContainerRunningAsync(string containerId)
        {
            try
            {
                var status = await ExecuteDockerCommandAsync($"inspect -f '{{{{.State.Running}}}}' {containerId}");
                return status.Trim().ToLower() == "true";
            }
            catch
            {
                return false;
            }
        }

        private async Task<string> ExecuteDockerCommandAsync(string arguments)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = _dockerCommand,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = new Process { StartInfo = startInfo };
            var output = new StringBuilder();
            var error = new StringBuilder();

            process.OutputDataReceived += (sender, e) =>
            {
                if (e.Data != null)
                    output.AppendLine(e.Data);
            };

            process.ErrorDataReceived += (sender, e) =>
            {
                if (e.Data != null)
                    error.AppendLine(e.Data);
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
            {
                throw new DockerOperationException(
                    $"Docker command failed with exit code {process.ExitCode}. Error: {error}");
            }

            return output.ToString();
        }
    }

    public class DockerOperationException : Exception
    {
        public DockerOperationException(string message) : base(message)
        {
        }
    }
}

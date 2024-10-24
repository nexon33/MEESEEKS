using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text;
using MEESEEKS.Interfaces;
using MEESEEKS.Models;

namespace MEESEEKS.Core
{
    public class DockerOperations : IDockerOperations
    {
        private readonly string _dockerCommand = "docker";

        public async Task<string> CreateContainerAsync(AgentConfiguration config)
        {
            var args = new StringBuilder();
            args.Append("create ");
            
            // Set resource limits
            if (config.ResourceLimits != null)
            {
                args.Append($"--cpus={config.ResourceLimits.CpuLimit} ");
                args.Append($"--memory={config.ResourceLimits.MemoryLimitMB}m ");
            }

            // Set environment variables
            if (config.EnvironmentVariables != null)
            {
                foreach (var env in config.EnvironmentVariables)
                {
                    args.Append($"-e {env.Key}={env.Value} ");
                }
            }

            // Mount solution directory
            args.Append($"-v {config.WorkingDirectory}:/workspace ");
            
            // Set working directory
            args.Append("-w /workspace ");
            
            // Use .NET SDK image
            args.Append(config.ContainerImage ?? "mcr.microsoft.com/dotnet/sdk:7.0");

            var result = await ExecuteDockerCommandAsync(args.ToString());
            return result.Trim();
        }

        public async Task StartContainerAsync(string containerId)
        {
            await ExecuteDockerCommandAsync($"start {containerId}");
        }

        public async Task StopContainerAsync(string containerId)
        {
            await ExecuteDockerCommandAsync($"stop {containerId}");
            await ExecuteDockerCommandAsync($"rm {containerId}");
        }

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

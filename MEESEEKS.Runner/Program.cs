﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using MEESEEKS.Core;
using MEESEEKS.Interfaces;
using MEESEEKS.Models.Agent;
using MEESEEKS.Models.Docker;
using MEESEEKS.Models.Task;
using MEESEEKS.Models.CodeGeneration;

namespace MEESEEKS.Runner
{
    /// <summary>
    /// Demonstrates the MEESEEKS agent system functionality by running a sample code generation task.
    /// This class serves as the entry point for the MEESEEKS Runner application, showcasing the integration
    /// of various components including Docker containerization, code generation, and agent communication.
    /// </summary>
    /// <remarks>
    /// The Program class orchestrates the following key operations:
    /// <list type="bullet">
    /// <item><description>Configures and initializes a Docker container for the MEESEEKS agent</description></item>
    /// <item><description>Sets up the agent with necessary dependencies and capabilities</description></item>
    /// <item><description>Executes a sample code generation task</description></item>
    /// <item><description>Handles task results and performs cleanup</description></item>
    /// </list>
    /// </remarks>
    public class Program
    {
        /// <summary>
        /// The main entry point for the MEESEEKS Runner application.
        /// </summary>
        /// <param name="args">Command line arguments (not currently used)</param>
        /// <returns>A task representing the asynchronous operation of the main program</returns>
        /// <remarks>
        /// This method performs the following steps:
        /// <list type="number">
        /// <item><description>Creates and configures a Docker container for the agent</description></item>
        /// <item><description>Initializes the agent with required dependencies</description></item>
        /// <item><description>Executes a sample code generation task</description></item>
        /// <item><description>Handles the task results and performs cleanup</description></item>
        /// </list>
        /// </remarks>
        /// <exception cref="Exception">Thrown when an error occurs during execution</exception>
        public static async Task Main(string[] args)
        {
            try
            {
                // Create Docker container configuration with detailed settings for resource management,
                // networking, health checks, and volume mapping
                var containerConfig = new DockerContainerConfig
                {
                    Id = Guid.NewGuid().ToString(),
                    ContainerName = "meeseeks-agent",
                    ImageName = "mcr.microsoft.com/dotnet/sdk",
                    ImageTag = "7.0",
                    WorkingDirectory = "/app",
                    NetworkConfig = new NetworkConfig 
                    { 
                        NetworkName = "bridge",
                        EnableNetworking = true
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
                    },
                    EntryPoint = "dotnet",
                    Volumes = new List<VolumeMapping>
                    {
                        new VolumeMapping
                        {
                            HostPath = "/home/user/Documents/MEESEEKS",
                            ContainerPath = "/app"
                        }
                    }
                };

                // Configure the agent with specific capabilities and container settings
                var agentConfig = new Models.Agent.AgentConfiguration
                {
                    Name = "CodeGeneratorAgent",
                    Description = "Agent for generating C# code",
                    ContainerConfig = containerConfig,
                    Capabilities = new HashSet<string> { "CodeGeneration", "CodeAnalysis" }
                };

                // Initialize MSBuild workspace for code analysis and generation
                using var workspace = MSBuildWorkspace.Create();

                // Initialize core dependencies required for agent operation
                var dockerOps = new DockerOperations();
                var gitOps = new GitOperations();
                var codeAnalyzer = new CodeAnalyzer();
                var codeGenerator = new CodeGenerator(workspace);
                var solutionManager = new SolutionManager();
                var communication = new AgentCommunication();

                // Create and initialize the MEESEEKS agent with all required dependencies
                var agent = new MeeseeksAgent(
                    agentConfig,
                    dockerOps,
                    gitOps,
                    codeAnalyzer,
                    codeGenerator,
                    solutionManager,
                    communication);

                await agent.InitializeContainerAsync();

                // Define a sample code generation task for demonstration
                var task = new Models.Task.AgentTask
                {
                    Id = Guid.NewGuid(),
                    Description = "Generate a sample calculator class with interface and tests",
                    TaskType = "CodeGeneration",
                    Priority = 1,
                    Parameters = new Dictionary<string, object>
                    {
                        { "className", "Calculator" },
                        { "namespace", "MEESEEKS.Sample" },
                        { "projectName", "MEESEEKS.Sample" }
                    }
                };

                // Execute the task and handle the results
                Console.WriteLine($"Executing task: {task.Description}");
                var result = await agent.ExecuteTaskAsync(task);

                if (result.Success)
                {
                    Console.WriteLine("Task completed successfully!");
                    if (result.Results.TryGetValue("generatedCode", out var generatedCode))
                    {
                        Console.WriteLine("\nGenerated Code:");
                        Console.WriteLine(generatedCode);
                    }
                    
                    if (!string.IsNullOrEmpty(result.Output))
                    {
                        Console.WriteLine("\nTask Output:");
                        Console.WriteLine(result.Output);
                    }
                }
                else
                {
                    Console.WriteLine($"Task failed: {result.Message}");
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Error: {error}");
                    }
                }

                // Cleanup
                await agent.ShutdownAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}

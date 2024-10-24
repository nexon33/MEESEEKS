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
                            Nameservers = new List<string> { "8.8

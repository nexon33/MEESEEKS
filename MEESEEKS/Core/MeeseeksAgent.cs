using System;
using System.Threading.Tasks;
using MEESEEKS.Interfaces;
using MEESEEKS.Models.Agent;
using MEESEEKS.Models.Task;
using MEESEEKS.Models.Docker;
using MEESEEKS.Models.CodeAnalysis;
using MEESEEKS.Models.CodeGeneration;

namespace MEESEEKS.Core
{
    /// <summary>
    /// Represents a MEESEEKS agent that performs code analysis, generation, and modification tasks
    /// within an isolated Docker container environment. This agent can analyze code, generate new code,
    /// and spawn additional agents to handle complex tasks.
    /// </summary>
    public partial class MeeseeksAgent : IAgent, IDisposable
    {
        private readonly IDockerOperations _dockerOps;
        private readonly IGitOperations _gitOps;
        private readonly ICodeAnalyzer _codeAnalyzer;
        private readonly ICodeGenerator _codeGenerator;
        private readonly ISolutionManager _solutionManager;
        private readonly IAgentCommunication _communication;
        private readonly AgentConfiguration _config;
        private string? _containerId;
        private bool _disposed;

        /// <summary>
        /// Gets the unique identifier of this agent.
        /// </summary>
        public string Id => _config.Id.ToString();

        /// <summary>
        /// Gets the current status of the agent.
        /// </summary>
        public AgentStatus Status { get; private set; }

        /// <summary>
        /// Initializes a new instance of the MeeseeksAgent class.
        /// </summary>
        /// <param name="config">Configuration settings for the agent.</param>
        /// <param name="dockerOps">Docker operations service.</param>
        /// <param name="gitOps">Git operations service.</param>
        /// <param name="codeAnalyzer">Code analysis service.</param>
        /// <param name="codeGenerator">Code generation service.</param>
        /// <param name="solutionManager">Solution management service.</param>
        /// <param name="communication">Agent communication service.</param>
        /// <exception cref="ArgumentNullException">Thrown when any required dependency is null.</exception>
        public MeeseeksAgent(
            AgentConfiguration config,
            IDockerOperations dockerOps,
            IGitOperations gitOps,
            ICodeAnalyzer codeAnalyzer,
            ICodeGenerator codeGenerator,
            ISolutionManager solutionManager,
            IAgentCommunication communication)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _dockerOps = dockerOps ?? throw new ArgumentNullException(nameof(dockerOps));
            _gitOps = gitOps ?? throw new ArgumentNullException(nameof(gitOps));
            _codeAnalyzer = codeAnalyzer ?? throw new ArgumentNullException(nameof(codeAnalyzer));
            _codeGenerator = codeGenerator ?? throw new ArgumentNullException(nameof(codeGenerator));
            _solutionManager = solutionManager ?? throw new ArgumentNullException(nameof(solutionManager));
            _communication = communication ?? throw new ArgumentNullException(nameof(communication));
            Status = AgentStatus.Initializing;
        }

        /// <summary>
        /// Creates and initializes a new agent instance.
        /// </summary>
        /// <param name="config">Configuration for the new agent.</param>
        /// <returns>The newly created agent instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown when config is null.</exception>
        public async Task<IAgent> SpawnNewAgentAsync(AgentConfiguration config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));

            var newAgent = new MeeseeksAgent(
                config,
                _dockerOps,
                _gitOps,
                _codeAnalyzer,
                _codeGenerator,
                _solutionManager,
                _communication);

            await newAgent.InitializeContainerAsync();
            await _communication.RegisterAgentAsync(newAgent);
            return newAgent;
        }
    }
}

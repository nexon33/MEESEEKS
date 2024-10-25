using System;
using System.Threading.Tasks;
using MEESEEKS.Interfaces;
using MEESEEKS.Models.Agent;

namespace MEESEEKS.Core
{
    /// <summary>
    /// Main agent class that handles task execution and coordination.
    /// </summary>
    public partial class MeeseeksAgent : IAgent, IDisposable
    {
        private readonly AgentConfiguration _config;
        private readonly IDockerOperations _dockerOperations;
        private readonly IGitOperations _gitOperations;
        private readonly ICodeAnalyzer _codeAnalyzer;
        private readonly ICodeGenerator _codeGenerator;
        private readonly ISolutionManager _solutionManager;
        private readonly IAgentCommunication _communication;
        private string? _containerId;
        private bool _disposed;

        /// <summary>
        /// Gets the unique identifier for this agent.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets the current status of the agent.
        /// </summary>
        public AgentStatus Status { get; private set; }

        /// <summary>
        /// Initializes a new instance of the MeeseeksAgent class.
        /// </summary>
        public MeeseeksAgent(
            AgentConfiguration config,
            IDockerOperations dockerOperations,
            IGitOperations gitOperations,
            ICodeAnalyzer codeAnalyzer,
            ICodeGenerator codeGenerator,
            ISolutionManager solutionManager,
            IAgentCommunication communication)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _dockerOperations = dockerOperations ?? throw new ArgumentNullException(nameof(dockerOperations));
            _gitOperations = gitOperations ?? throw new ArgumentNullException(nameof(gitOperations));
            _codeAnalyzer = codeAnalyzer ?? throw new ArgumentNullException(nameof(codeAnalyzer));
            _codeGenerator = codeGenerator ?? throw new ArgumentNullException(nameof(codeGenerator));
            _solutionManager = solutionManager ?? throw new ArgumentNullException(nameof(solutionManager));
            _communication = communication ?? throw new ArgumentNullException(nameof(communication));

            Id = Guid.NewGuid().ToString();
            Status = AgentStatus.Ready;
        }

        /// <summary>
        /// Spawns a new agent with the specified configuration.
        /// </summary>
        /// <param name="config">The configuration for the new agent.</param>
        /// <returns>A task representing the asynchronous agent creation operation.</returns>
        public async Task<IAgent> SpawnNewAgentAsync(AgentConfiguration config)
        {
            // Create a new agent instance with the same dependencies
            var newAgent = new MeeseeksAgent(
                config,
                _dockerOperations,
                _gitOperations,
                _codeAnalyzer,
                _codeGenerator,
                _solutionManager,
                _communication
            );

            await newAgent.InitializeContainerAsync();
            return newAgent;
        }
    }
}

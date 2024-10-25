using System;
using System.Threading.Tasks;
using MEESEEKS.Models.Task;
using MEESEEKS.Models.Agent;

namespace MEESEEKS.Core
{
    public partial class MeeseeksAgent
    {
        /// <summary>
        /// Executes a task asynchronously within the agent's container.
        /// </summary>
        /// <param name="task">The task to execute.</param>
        /// <returns>The result of the task execution.</returns>
        /// <exception cref="ArgumentNullException">Thrown when task is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when agent is not in ready state.</exception>
        public async Task<AgentResult> ExecuteTaskAsync(AgentTask task)
        {
            if (task == null) throw new ArgumentNullException(nameof(task));
            if (Status != AgentStatus.Ready)
            {
                throw new InvalidOperationException($"Agent is not ready. Current status: {Status}");
            }

            try
            {
                Status = AgentStatus.Working;
                var result = await ProcessTaskAsync(task);
                Status = AgentStatus.Ready;
                return result;
            }
            catch (Exception ex)
            {
                Status = AgentStatus.Error;
                return new AgentResult
                {
                    Success = false,
                    Message = ex.Message,
                    Errors = { ex.ToString() }
                };
            }
        }

        /// <summary>
        /// Routes the task to the appropriate processing method based on its type.
        /// </summary>
        /// <param name="task">The task to process.</param>
        /// <returns>The result of the task processing.</returns>
        /// <exception cref="NotSupportedException">Thrown when task type is not supported.</exception>
        private async Task<AgentResult> ProcessTaskAsync(AgentTask task)
        {
            return task.TaskType switch
            {
                "CodeAnalysis" => await ProcessCodeAnalysisTaskAsync(task),
                "CodeGeneration" => await ProcessCodeGenerationTaskAsync(task),
                "Refactoring" => await ProcessRefactoringTaskAsync(task),
                "Testing" => await ProcessTestingTaskAsync(task),
                _ => throw new NotSupportedException($"Task type {task.TaskType} is not supported")
            };
        }
    }
}

using System;
using System.Threading.Tasks;
using System.IO;
using MEESEEKS.Models.Task;

namespace MEESEEKS.Core
{
    public partial class MeeseeksAgent
    {
        /// <summary>
        /// Processes a testing task by running tests in the specified test project.
        /// </summary>
        /// <param name="task">The testing task to process.</param>
        /// <returns>The result of the test execution.</returns>
        /// <exception cref="ArgumentException">Thrown when required parameters are missing.</exception>
        /// <exception cref="FileNotFoundException">Thrown when test project file is not found.</exception>
        /// <exception cref="InvalidOperationException">Thrown when project path is invalid.</exception>
        private async Task<AgentResult> ProcessTestingTaskAsync(AgentTask task)
        {
            var projectPath = task.Parameters.GetValueOrDefault("testProject")?.ToString()
                ?? throw new ArgumentException("testProject parameter is required");

            if (!File.Exists(projectPath))
            {
                throw new FileNotFoundException("Test project file not found", projectPath);
            }

            var solutionPath = Path.GetDirectoryName(projectPath)
                ?? throw new InvalidOperationException("Invalid project path");

            await _solutionManager.LoadSolutionAsync(solutionPath);
            await _solutionManager.RunTestsAsync();

            return new AgentResult
            {
                Success = true,
                Message = "Test execution completed successfully",
                Results = { ["TestProject"] = projectPath }
            };
        }
    }
}

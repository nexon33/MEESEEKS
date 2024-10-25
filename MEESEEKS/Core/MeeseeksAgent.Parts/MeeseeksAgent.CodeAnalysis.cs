using System;
using System.Threading.Tasks;
using System.IO;
using MEESEEKS.Models.Task;

namespace MEESEEKS.Core
{
    public partial class MeeseeksAgent
    {
        /// <summary>
        /// Processes a code analysis task by analyzing the provided solution.
        /// </summary>
        /// <param name="task">The code analysis task to process.</param>
        /// <returns>The result of the code analysis.</returns>
        /// <exception cref="ArgumentException">Thrown when required parameters are missing.</exception>
        /// <exception cref="FileNotFoundException">Thrown when solution file is not found.</exception>
        private async Task<AgentResult> ProcessCodeAnalysisTaskAsync(AgentTask task)
        {
            var solutionPath = task.Parameters.GetValueOrDefault("solutionPath")?.ToString()
                ?? throw new ArgumentException("solutionPath parameter is required");

            if (!File.Exists(solutionPath))
            {
                throw new FileNotFoundException("Solution file not found", solutionPath);
            }
            
            var codeContent = await File.ReadAllTextAsync(solutionPath);
            var analysisResult = await _codeAnalyzer.AnalyzeCodeAsync(codeContent);
            
            return new AgentResult
            {
                Success = true,
                Message = "Code analysis completed successfully",
                Results = { ["Analysis"] = analysisResult }
            };
        }
    }
}

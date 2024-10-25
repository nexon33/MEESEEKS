using System;
using System.Threading.Tasks;
using System.IO;
using MEESEEKS.Models.Task;

namespace MEESEEKS.Core
{
    public partial class MeeseeksAgent
    {
        /// <summary>
        /// Processes a refactoring task by analyzing and modifying the provided code.
        /// </summary>
        /// <param name="task">The refactoring task to process.</param>
        /// <returns>The result of the refactoring operation.</returns>
        /// <exception cref="ArgumentException">Thrown when required parameters are missing.</exception>
        /// <exception cref="FileNotFoundException">Thrown when source file is not found.</exception>
        private async Task<AgentResult> ProcessRefactoringTaskAsync(AgentTask task)
        {
            var filePath = task.Parameters.GetValueOrDefault("filePath")?.ToString()
                ?? throw new ArgumentException("filePath parameter is required");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Source file not found", filePath);
            }
            
            var codeContent = await File.ReadAllTextAsync(filePath);
            var analysisResult = await _codeAnalyzer.AnalyzeCodeAsync(codeContent);
            var modifications = await _codeAnalyzer.GenerateCodeModificationAsync(analysisResult);

            return new AgentResult
            {
                Success = true,
                Message = "Code refactoring completed successfully",
                Results = { ["Modifications"] = modifications }
            };
        }
    }
}

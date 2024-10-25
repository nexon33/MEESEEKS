using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MEESEEKS.Models.Task;
using MEESEEKS.Models.CodeGeneration;

namespace MEESEEKS.Core
{
    public partial class MeeseeksAgent
    {
        /// <summary>
        /// Processes a code generation task by generating code based on the provided parameters.
        /// </summary>
        /// <param name="task">The code generation task to process.</param>
        /// <returns>The result of the code generation.</returns>
        private async Task<AgentResult> ProcessCodeGenerationTaskAsync(AgentTask task)
        {
            var description = task.Parameters.GetValueOrDefault("description")?.ToString() ?? "Generated code";
            var language = task.Parameters.GetValueOrDefault("language")?.ToString() ?? "csharp";
            var framework = task.Parameters.GetValueOrDefault("framework")?.ToString() ?? string.Empty;

            var request = new CodeGenerationRequest
            {
                Description = description,
                Language = language,
                Framework = framework,
                GenerateTests = task.Parameters.TryGetValue("generateTests", out var genTests) && 
                              bool.TryParse(genTests?.ToString(), out var testVal) && testVal,
                GenerateDocumentation = task.Parameters.TryGetValue("generateDocs", out var genDocs) && 
                                      bool.TryParse(genDocs?.ToString(), out var docVal) && docVal,
                Requirements = new List<string> { "Clean code", "SOLID principles", "XML documentation" }
            };

            var generatedCode = await _codeGenerator.GenerateCodeAsync(request);
            return new AgentResult
            {
                Success = true,
                Message = "Code generation completed successfully",
                Results = { ["GeneratedCode"] = generatedCode }
            };
        }
    }
}

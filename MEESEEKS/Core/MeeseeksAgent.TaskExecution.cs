using System;
using System.Threading.Tasks;
using System.IO;
using MEESEEKS.Models.Task;
using MEESEEKS.Models.Agent;
using MEESEEKS.Models.CodeGeneration;
using MEESEEKS.Models.CodeAnalysis;

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

        private async Task<AgentResult> ProcessCodeGenerationTaskAsync(AgentTask task)
        {
            var description = task.Parameters.GetValueOrDefault("description")?.ToString() ?? "Generated code";
            var language = task.Parameters.GetValueOrDefault("language")?.ToString() ?? "csharp";
            var framework = task.Parameters.GetValueOrDefault("framework")?.ToString();

            var request = new CodeGenerationRequest
            {
                Description = description,
                Language = language,
                Framework = framework,
                GenerateTests = task.Parameters.TryGetValue("generateTests", out var genTests) && 
                              bool.TryParse(genTests?.ToString(), out var testVal) && testVal,
                GenerateDocumentation = task.Parameters.TryGetValue("generateDocs", out var genDocs) && 
                                      bool.TryParse(genDocs?.ToString(), out var docVal) && docVal,
                Requirements = { "Clean code", "SOLID principles", "XML documentation" }
            };

            var generatedCode = await _codeGenerator.GenerateCodeAsync(request);
            return new AgentResult
            {
                Success = true,
                Message = "Code generation completed successfully",
                Results = { ["GeneratedCode"] = generatedCode }
            };
        }

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
            await _solutionManager.RunTest

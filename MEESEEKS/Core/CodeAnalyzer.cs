using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using MEESEEKS.Core.Analysis;
using MEESEEKS.Interfaces;
using MEESEEKS.Models.CodeAnalysis;

namespace MEESEEKS.Core
{
    /// <summary>
    /// Provides functionality for analyzing C# code using the Roslyn compiler platform.
    /// </summary>
    public class CodeAnalyzer : ICodeAnalyzer
    {
        private readonly CodeComplexityAnalyzer _complexityAnalyzer = new();
        private readonly CodeDefinitionAnalyzer _definitionAnalyzer = new();
        private readonly CodeLocationMapper _locationMapper = new();

        /// <summary>
        /// Analyzes the provided code content asynchronously.
        /// </summary>
        /// <param name="codeContent">The source code content to analyze.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentException">Thrown when code content is null or empty.</exception>
        public async Task<CodeAnalysisResult> AnalyzeCodeAsync(string codeContent)
        {
            if (string.IsNullOrEmpty(codeContent))
                throw new ArgumentException("Code content cannot be null or empty", nameof(codeContent));

            var tree = CSharpSyntaxTree.ParseText(codeContent);
            var root = await tree.GetRootAsync();
            var compilation = CSharpCompilation.Create("Analysis")
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                .AddSyntaxTrees(tree);

            var metrics = await Task.Run(() => _complexityAnalyzer.AnalyzeMethodComplexity(root));
            var issues = GetIssues(compilation);
            var context = await CollectContext(root);

            return new CodeAnalysisResult
            {
                FilePath = tree.FilePath ?? string.Empty,
                Issues = issues,
                Metrics = new CodeMetrics { Metrics = metrics.ToList() },
                Context = context,
                AnalyzedAt = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Generates code modifications based on the analysis results.
        /// </summary>
        /// <param name="analysis">The code analysis result containing the findings.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when analysis is null.</exception>
        public async Task<CodeModification> GenerateCodeModificationAsync(CodeAnalysisResult analysis)
        {
            if (analysis == null)
                throw new ArgumentNullException(nameof(analysis));

            return await Task.Run(() => new CodeModification
            {
                FilePath = analysis.FilePath,
                OriginalContent = string.Empty,
                ModifiedContent = string.Empty,
                Changes = new List<CodeChange>(),
                ModifiedAt = DateTime.UtcNow
            });
        }

        private List<CodeIssue> GetIssues(CSharpCompilation compilation)
        {
            var diagnostics = compilation.GetDiagnostics();
            return diagnostics.Select(d => new CodeIssue
            {
                Id = d.Id,
                Description = d.GetMessage(),
                Severity = _locationMapper.MapSeverity(d.Severity),
                Location = _locationMapper.MapLocation(d.Location) ?? new CodeLocation(),
                SuggestedFix = string.Empty
            }).ToList();
        }

        private async Task<CodeContext> CollectContext(SyntaxNode root)
        {
            var context = new CodeContext();

            var classesTask = Task.Run(() => _definitionAnalyzer.GetClassDefinitions(root).ToList());
            var methodsTask = Task.Run(() => _definitionAnalyzer.GetMethodDefinitions(root).ToList());
            var usingsTask = Task.Run(() => _definitionAnalyzer.GetUsingDirectives(root).ToList());

            await Task.WhenAll(classesTask, methodsTask, usingsTask);

            context.Add("Classes", classesTask.Result);
            context.Add("Methods", methodsTask.Result);
            context.Add("Usings", usingsTask.Result);

            return context;
        }
    }
}

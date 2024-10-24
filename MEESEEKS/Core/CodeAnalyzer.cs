using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MEESEEKS.Interfaces;
using MEESEEKS.Models;

namespace MEESEEKS.Core
{
    public class CodeAnalyzer : ICodeAnalyzer
    {
        public async Task<CodeAnalysisResult> AnalyzeCodeAsync(string codeContent)
        {
            if (string.IsNullOrEmpty(codeContent))
                throw new ArgumentException("Code content cannot be null or empty", nameof(codeContent));

            // Directly call the async method without Task.Run
            return await AnalyzeCodeInternalAsync(codeContent);
        }

        private async Task<CodeAnalysisResult> AnalyzeCodeInternalAsync(string codeContent)
        {
            var tree = CSharpSyntaxTree.ParseText(codeContent);
            var root = await tree.GetRootAsync();
            var compilation = CSharpCompilation.Create("Analysis")
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                .AddSyntaxTrees(tree);

            var model = compilation.GetSemanticModel(tree);
            var issues = new List<CodeIssue>();
            var metrics = new List<CodeMetric>();

            // Analyze code complexity
            var methodComplexity = await Task.Run(() => AnalyzeMethodComplexity(root));
            metrics.AddRange(methodComplexity);

            // Analyze code issues
            var diagnostics = compilation.GetDiagnostics();
            issues.AddRange(diagnostics.Select(d => new CodeIssue
            {
                Id = d.Id,
                Description = d.GetMessage(),
                Severity = MapSeverity(d.Severity),
                Location = MapLocation(d.Location) ?? new CodeLocation
                {
                    FilePath = string.Empty,
                    StartLine = 0,
                    EndLine = 0,
                    StartColumn = 0,
                    EndColumn = 0
                },
                SuggestedFix = string.Empty
            }));

            // Collect context information
            var context = new Dictionary<string, object>
            {
                { "Classes", await Task.Run(() => GetClassDefinitions(root).ToList()) },
                { "Methods", await Task.Run(() => GetMethodDefinitions(root).ToList()) },
                { "Usings", await Task.Run(() => GetUsingDirectives(root).ToList()) }
            };

            return new CodeAnalysisResult
            {
                FilePath = tree.FilePath ?? string.Empty,
                Issues = issues,
                Metrics = metrics,
                Context = context
            };
        }

        public async Task<CodeModification> GenerateCodeModificationAsync(CodeAnalysisResult analysis)
        {
            if (analysis == null)
                throw new ArgumentNullException(nameof(analysis));

            return await Task.Run(() => new CodeModification
            {
                FilePath = analysis.FilePath,
                OriginalContent = string.Empty,
                ModifiedContent = string.Empty,
                Changes = new List<CodeChange>()
            });
        }

        private IEnumerable<CodeMetric> AnalyzeMethodComplexity(SyntaxNode root)
        {
            if (root == null)
                throw new ArgumentNullException(nameof(root));

            var metrics = new List<CodeMetric>();
            var methods = root.DescendantNodes().OfType<MethodDeclarationSyntax>();

            foreach (var method in methods)
            {
                var complexity = CalculateCyclomaticComplexity(method);
                metrics.Add(new CodeMetric
                {
                    Name = $"Cyclomatic Complexity - {method.Identifier.Text}",
                    Value = complexity,
                    Unit = "paths"
                });
            }

            return metrics;
        }

        private int CalculateCyclomaticComplexity(MethodDeclarationSyntax method)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));

            var complexity = 1; // Base complexity

            complexity += method.DescendantNodes().Count(n =>
                n is IfStatementSyntax ||
                n is WhileStatementSyntax ||
                n is ForStatementSyntax ||
                n is ForEachStatementSyntax ||
                n is CaseSwitchLabelSyntax ||
                n is CatchClauseSyntax ||
                n is ConditionalExpressionSyntax ||
                n is BinaryExpressionSyntax bex &&
                (bex.OperatorToken.ValueText == "&&" ||
                 bex.OperatorToken.ValueText == "||"));

            return complexity;
        }

        private IssueSeverity MapSeverity(DiagnosticSeverity severity)
        {
            return severity switch
            {
                DiagnosticSeverity.Error => IssueSeverity.Error,
                DiagnosticSeverity.Warning => IssueSeverity.Warning,
                DiagnosticSeverity.Info => IssueSeverity.Info,
                DiagnosticSeverity.Hidden => IssueSeverity.Info,
                _ => IssueSeverity.Info
            };
        }

        private CodeLocation? MapLocation(Location? location)
        {
            if (location == null)
                return null;

            var span = location.GetLineSpan();
            return new CodeLocation
            {
                FilePath = span.Path ?? string.Empty,
                StartLine = span.StartLinePosition.Line + 1,
                EndLine = span.EndLinePosition.Line + 1,
                StartColumn = span.StartLinePosition.Character + 1,
                EndColumn = span.EndLinePosition.Character + 1
            };
        }

        private IEnumerable<string> GetClassDefinitions(SyntaxNode root)
        {
            if (root == null)
                throw new ArgumentNullException(nameof(root));

            return root.DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                .Select(c => c.Identifier.Text)
                .Where(name => !string.IsNullOrEmpty(name));
        }

        private IEnumerable<string> GetMethodDefinitions(SyntaxNode root)
        {
            if (root == null)
                throw new ArgumentNullException(nameof(root));

            return root.DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .Select(m => m.Identifier.Text)
                .Where(name => !string.IsNullOrEmpty(name));
        }

        private IEnumerable<string> GetUsingDirectives(SyntaxNode root)
        {
            if (root == null)
                throw new ArgumentNullException(nameof(root));

            return root.DescendantNodes()
                .OfType<UsingDirectiveSyntax>()
                .Select(u => u.Name?.ToString() ?? string.Empty)
                .Where(name => !string.IsNullOrEmpty(name));
        }
    }
}

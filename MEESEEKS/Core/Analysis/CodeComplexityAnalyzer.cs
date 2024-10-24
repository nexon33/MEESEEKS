using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MEESEEKS.Models.CodeAnalysis;

namespace MEESEEKS.Core.Analysis
{
    /// <summary>
    /// Analyzes code complexity metrics using Roslyn.
    /// </summary>
    internal class CodeComplexityAnalyzer
    {
        /// <summary>
        /// Analyzes the cyclomatic complexity of methods in the syntax tree.
        /// </summary>
        /// <param name="root">The syntax tree root node.</param>
        /// <returns>A collection of code metrics representing method complexities.</returns>
        /// <exception cref="ArgumentNullException">Thrown when root is null.</exception>
        public IEnumerable<CodeMetric> AnalyzeMethodComplexity(SyntaxNode root)
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

        /// <summary>
        /// Calculates the cyclomatic complexity of a method.
        /// </summary>
        /// <param name="method">The method declaration syntax node.</param>
        /// <returns>The calculated cyclomatic complexity value.</returns>
        /// <exception cref="ArgumentNullException">Thrown when method is null.</exception>
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MEESEEKS.Core.Analysis
{
    /// <summary>
    /// Analyzes code definitions using Roslyn.
    /// </summary>
    internal class CodeDefinitionAnalyzer
    {
        /// <summary>
        /// Gets all class definitions from the syntax tree.
        /// </summary>
        /// <param name="root">The syntax tree root node.</param>
        /// <returns>A collection of class names.</returns>
        /// <exception cref="ArgumentNullException">Thrown when root is null.</exception>
        public IEnumerable<string> GetClassDefinitions(SyntaxNode root)
        {
            if (root == null)
                throw new ArgumentNullException(nameof(root));

            return root.DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                .Select(c => c.Identifier.Text)
                .Where(name => !string.IsNullOrEmpty(name));
        }

        /// <summary>
        /// Gets all method definitions from the syntax tree.
        /// </summary>
        /// <param name="root">The syntax tree root node.</param>
        /// <returns>A collection of method names.</returns>
        /// <exception cref="ArgumentNullException">Thrown when root is null.</exception>
        public IEnumerable<string> GetMethodDefinitions(SyntaxNode root)
        {
            if (root == null)
                throw new ArgumentNullException(nameof(root));

            return root.DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .Select(m => m.Identifier.Text)
                .Where(name => !string.IsNullOrEmpty(name));
        }

        /// <summary>
        /// Gets all using directives from the syntax tree.
        /// </summary>
        /// <param name="root">The syntax tree root node.</param>
        /// <returns>A collection of using directive names.</returns>
        /// <exception cref="ArgumentNullException">Thrown when root is null.</exception>
        public IEnumerable<string> GetUsingDirectives(SyntaxNode root)
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

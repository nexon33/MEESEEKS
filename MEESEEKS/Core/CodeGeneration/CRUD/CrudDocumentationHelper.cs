using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace MEESEEKS.Core.CodeGeneration.CRUD
{
    /// <summary>
    /// Provides helper methods for generating XML documentation for CRUD operations.
    /// </summary>
    internal static class CrudDocumentationHelper
    {
        /// <summary>
        /// Adds XML documentation to a method declaration.
        /// </summary>
        public static MethodDeclarationSyntax AddMethodDocumentation(
            MethodDeclarationSyntax method,
            string summary,
            params (string name, string description)[] parameters)
        {
            var lines = new List<string>
            {
                "/// <summary>",
                $"/// {summary}",
                "/// </summary>"
            };

            foreach (var param in parameters)
            {
                lines.Add($"/// <param name=\"{param.name}\">{param.description}</param>");
            }

            lines.Add("/// <returns>A task that represents the asynchronous operation.</returns>");

            var trivia = string.Join("\n", lines);
            return method.WithLeadingTrivia(
                SyntaxFactory.ParseLeadingTrivia(trivia));
        }
    }
}

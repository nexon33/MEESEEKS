using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MEESEEKS.Core.CodeGeneration.CRUD
{
    /// <summary>
    /// Generates the Delete method for CRUD operations.
    /// </summary>
    public static class DeleteMethodGenerator
    {
        /// <summary>
        /// Generates a method declaration for deleting an entity asynchronously.
        /// </summary>
        /// <returns>A method declaration syntax for the DeleteAsync method.</returns>
        public static MethodDeclarationSyntax Generate()
        {
            var method = SyntaxFactory.MethodDeclaration(
                SyntaxFactory.GenericName(SyntaxFactory.Identifier("Task"))

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MEESEEKS.Core.CodeGeneration.CRUD
{
    /// <summary>
    /// Generates the Read method for CRUD operations.
    /// </summary>
    public static class ReadMethodGenerator
    {
        /// <summary>
        /// Generates a method declaration for retrieving an entity by ID asynchronously.
        /// </summary>
        /// <returns>A method declaration syntax for the GetByIdAsync method.</returns>
        public static MethodDeclarationSyntax Generate()
        {
            var method = SyntaxFactory.MethodDeclaration(
                SyntaxFactory.GenericName(SyntaxFactory.Identifier("Task"))
                    .WithTypeArgumentList(
                        SyntaxFactory.TypeArgumentList(
                            SyntaxFactory.SingletonSeparatedList<TypeSyntax>(
                                SyntaxFactory.IdentifierName("T")))),
                SyntaxFactory.Identifier("GetByIdAsync"))
                .WithParameterList(
                    SyntaxFactory.ParameterList(
                        SyntaxFactory.SingletonSeparatedList(
                            SyntaxFactory.Parameter(
                                SyntaxFactory.Identifier("id"))
                                .WithType(
                                    SyntaxFactory.PredefinedType(
                                        SyntaxFactory.Token(SyntaxKind.IntKeyword))))));

            return CrudDocumentationHelper.AddMethodDocumentation(
                method,
                "Retrieves an entity by its ID from the system.",
                ("id", "The ID of the entity to retrieve"));
        }
    }
}

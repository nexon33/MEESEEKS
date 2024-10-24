using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MEESEEKS.Core.CodeGeneration.CRUD
{
    /// <summary>
    /// Generates the Create method for CRUD operations.
    /// </summary>
    public static class CreateMethodGenerator
    {
        /// <summary>
        /// Generates a method declaration for creating a new entity asynchronously.
        /// </summary>
        /// <returns>A method declaration syntax for the CreateAsync method.</returns>
        public static MethodDeclarationSyntax Generate()
        {
            var method = SyntaxFactory.MethodDeclaration(
                SyntaxFactory.GenericName(SyntaxFactory.Identifier("Task"))
                    .WithTypeArgumentList(
                        SyntaxFactory.TypeArgumentList(
                            SyntaxFactory.SingletonSeparatedList<TypeSyntax>(
                                SyntaxFactory.PredefinedType(
                                    SyntaxFactory.Token(SyntaxKind.VoidKeyword))))),
                SyntaxFactory.Identifier("CreateAsync"))
                .WithParameterList(
                    SyntaxFactory.ParameterList(
                        SyntaxFactory.SingletonSeparatedList(
                            SyntaxFactory.Parameter(
                                SyntaxFactory.Identifier("entity"))
                                .WithType(
                                    SyntaxFactory.IdentifierName("T")))));

            return CrudDocumentationHelper.AddMethodDocumentation(
                method,
                "Creates a new entity in the system.",
                ("entity", "The entity to create"));
        }
    }
}

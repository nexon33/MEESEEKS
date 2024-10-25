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
                    .WithTypeArgumentList(
                        SyntaxFactory.TypeArgumentList(
                            SyntaxFactory.SingletonSeparatedList<TypeSyntax>(
                                SyntaxFactory.PredefinedType(
                                    SyntaxFactory.Token(SyntaxKind.VoidKeyword))))),
                SyntaxFactory.Identifier("DeleteAsync"))
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
                "Deletes an entity from the system.",
                ("id", "The ID of the entity to delete"));
        }
    }
}

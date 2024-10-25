using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MEESEEKS.Core.CodeGeneration.CRUD
{
    /// <summary>
    /// Generates the List method for CRUD operations.
    /// </summary>
    public static class ListMethodGenerator
    {
        /// <summary>
        /// Generates a method declaration for retrieving all entities asynchronously.
        /// </summary>
        /// <returns>A method declaration syntax for the ListAsync method.</returns>
        public static MethodDeclarationSyntax Generate()
        {
            var method = SyntaxFactory.MethodDeclaration(
                SyntaxFactory.GenericName(SyntaxFactory.Identifier("Task"))
                    .WithTypeArgumentList(
                        SyntaxFactory.TypeArgumentList(
                            SyntaxFactory.SingletonSeparatedList<TypeSyntax>(
                                SyntaxFactory.GenericName(
                                    SyntaxFactory.Identifier("IEnumerable"))
                                    .WithTypeArgumentList(
                                        SyntaxFactory.TypeArgumentList(
                                            SyntaxFactory.SingletonSeparatedList<TypeSyntax>(
                                                SyntaxFactory.IdentifierName("T"))))))),
                SyntaxFactory.Identifier("ListAsync"));

            return CrudDocumentationHelper.AddMethodDocumentation(
                method,
                "Retrieves all entities from the system.");
        }
    }
}

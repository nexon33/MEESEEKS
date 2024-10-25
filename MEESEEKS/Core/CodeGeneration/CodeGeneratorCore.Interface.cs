using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MEESEEKS.Models.CodeGeneration;
using MEESEEKS.Core.CodeGeneration.CRUD;

namespace MEESEEKS.Core.CodeGeneration
{
    public partial class CodeGeneratorCore
    {
        private async Task<SourceFile> GenerateInterfaceFileAsync(CodeGenerationRequest request)
        {
            return await Task.Run(async () =>
            {
                var interfaceName = $"I{request.Description.Split(' ')[0]}";
                var methods = await CrudMethodGenerator.GenerateAllAsync();

                var interfaceDeclaration = SyntaxFactory.InterfaceDeclaration(interfaceName)
                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                    .AddMembers(methods.ToArray());

                if (request.GenerateDocumentation)
                {
                    var summary = $"Defines the contract for {request.Description}.";
                    var documentationTrivia = SyntaxFactory.TriviaList(
                        SyntaxFactory.Comment($"/// <summary>\n/// {summary}\n/// </summary>"));
                    interfaceDeclaration = interfaceDeclaration.WithLeadingTrivia(documentationTrivia);
                }

                var namespaceDeclaration = SyntaxFactory.NamespaceDeclaration(
                    SyntaxFactory.ParseName(request.Language))
                    .AddMembers(interfaceDeclaration);

                var compilationUnit = SyntaxFactory.CompilationUnit()
                    .AddUsings(
                        SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")),
                        SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Collections.Generic")),
                        SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Threading.Tasks")))
                    .AddMembers(namespaceDeclaration);

                return new SourceFile
                {
                    FilePath = $"{interfaceName}.cs",
                    Content = compilationUnit.ToFullString(),
                    Language = request.Language,
                    FileType = SourceFileType.Interface,
                    Namespace = request.Language,
                    DefinedClasses = new List<string> { interfaceName }
                };
            });
        }

        private async Task<string> GenerateInterfaceSourceCodeAsync(
            string interfaceName,
            List<MethodDefinition> methods,
            List<PropertyDefinition> properties)
        {
            return await Task.Run(() =>
            {
                var declaration = SyntaxFactory.InterfaceDeclaration(interfaceName)
                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));

                // Add methods
                var methodDeclarations = methods.Select(m => 
                    SyntaxFactory.MethodDeclaration(
                        SyntaxFactory.ParseTypeName(m.ReturnType),
                        SyntaxFactory.Identifier(m.Name))
                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword)));

                // Add properties
                var propertyDeclarations = properties.Select(p =>
                    SyntaxFactory.PropertyDeclaration(
                        SyntaxFactory.ParseTypeName(p.Type),
                        SyntaxFactory.Identifier(p.Name))
                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                    .AddAccessorListAccessors(
                        SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                            .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))));

                declaration = declaration
                    .AddMembers(methodDeclarations.ToArray())
                    .AddMembers(propertyDeclarations.ToArray());

                var namespaceDeclaration = SyntaxFactory.NamespaceDeclaration(
                    SyntaxFactory.ParseName("MEESEEKS.Generated"))
                    .AddMembers(declaration);

                var compilationUnit = SyntaxFactory.CompilationUnit()
                    .AddMembers(namespaceDeclaration);

                return compilationUnit.ToFullString();
            });
        }
    }
}

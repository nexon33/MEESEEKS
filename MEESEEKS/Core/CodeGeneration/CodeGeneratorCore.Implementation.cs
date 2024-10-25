using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MEESEEKS.Models.CodeGeneration;

namespace MEESEEKS.Core.CodeGeneration
{
    public partial class CodeGeneratorCore
    {
        private async Task<SourceFile> GenerateImplementationFileAsync(CodeGenerationRequest request)
        {
            return await Task.Run(() =>
            {
                var className = request.Description.Split(' ')[0];
                var interfaceName = $"I{className}";

                var classDeclaration = SyntaxFactory.ClassDeclaration(className)
                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                    .AddBaseListTypes(
                        SyntaxFactory.SimpleBaseType(
                            SyntaxFactory.IdentifierName(interfaceName)));

                var namespaceDeclaration = SyntaxFactory.NamespaceDeclaration(
                    SyntaxFactory.ParseName(request.Language))
                    .AddMembers(classDeclaration);

                var compilationUnit = SyntaxFactory.CompilationUnit()
                    .AddUsings(
                        SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")),
                        SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Threading.Tasks")))
                    .AddMembers(namespaceDeclaration);

                return new SourceFile
                {
                    FilePath = $"{className}.cs",
                    Content = compilationUnit.ToFullString(),
                    Language = request.Language,
                    FileType = SourceFileType.Class,
                    Namespace = request.Language,
                    DefinedClasses = new List<string> { className }
                };
            });
        }

        private async Task<List<MethodDefinition>> ExtractMethodDefinitionsAsync(ClassDeclarationSyntax classDeclaration)
        {
            return await Task.Run(() => new List<MethodDefinition>());
        }

        private async Task<List<PropertyDefinition>> ExtractPropertyDefinitionsAsync(ClassDeclarationSyntax classDeclaration)
        {
            return await Task.Run(() => new List<PropertyDefinition>());
        }
    }
}

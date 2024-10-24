using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MEESEEKS.Models.CodeGeneration;

namespace MEESEEKS.Core
{
    public partial class CodeGenerator
    {
        private async Task<SourceFile> GenerateInterfaceFileAsync(CodeGenerationRequest request)
        {
            var interfaceName = $"I{request.Description.Split(' ')[0]}";
            var interfaceDeclaration = await GenerateInterfaceDeclarationAsync(interfaceName, request);
            
            var namespaceDeclaration = SyntaxFactory.NamespaceDeclaration(
                SyntaxFactory.ParseName(request.Language))
                .AddMembers(interfaceDeclaration);

            var usings = new[]
            {
                "System",
                "System.Collections.Generic",
                "System.Threading.Tasks"
            }.Select(u => SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(u)));

            var compilationUnit = SyntaxFactory.CompilationUnit()
                .AddUsings(usings.ToArray())
                .AddMembers(namespaceDeclaration);

            var sourceCode = await Task.Run(() => compilationUnit.NormalizeWhitespace().ToFullString());

            return new SourceFile
            {
                FilePath = $"{interfaceName}.cs",
                Content = sourceCode,
                Language = request.Language,
                FileType = SourceFileType.Interface,
                Namespace = request.Language,
                DefinedClasses = new List<string> { interfaceName }
            };
        }

        private async Task<InterfaceDeclarationSyntax> GenerateInterfaceDeclarationAsync(string interfaceName, CodeGenerationRequest request)
        {
            var declaration = SyntaxFactory.InterfaceDeclaration(interfaceName)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));

            if (request.GenerateDocumentation)
            {
                declaration = AddXmlDocumentation(declaration, 
                    $"Defines the contract for {request.Description}.");
            }

            var methods = await GenerateMethodsFromDescriptionAsync(request);
            declaration = declaration.AddMembers(methods.ToArray());

            return declaration;
        }
    }
}

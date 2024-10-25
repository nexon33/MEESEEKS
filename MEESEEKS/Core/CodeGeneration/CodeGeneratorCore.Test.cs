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
        private async Task<SourceFile> GenerateTestFileAsync(CodeGenerationRequest request, SourceFile implementationFile)
        {
            return await Task.Run(() =>
            {
                var className = implementationFile.DefinedClasses[0];
                var testClassName = $"{className}Tests";

                var classDeclaration = SyntaxFactory.ClassDeclaration(testClassName)
                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));

                var namespaceDeclaration = SyntaxFactory.NamespaceDeclaration(
                    SyntaxFactory.ParseName($"{request.Language}.Tests"))
                    .AddMembers(classDeclaration);

                var compilationUnit = SyntaxFactory.CompilationUnit()
                    .AddUsings(
                        SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")),
                        SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Threading.Tasks")),
                        SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("Xunit")))
                    .AddMembers(namespaceDeclaration);

                return new SourceFile
                {
                    FilePath = $"{testClassName}.cs",
                    Content = compilationUnit.ToFullString(),
                    Language = request.Language,
                    FileType = SourceFileType.Test,
                    Namespace = $"{request.Language}.Tests",
                    DefinedClasses = new List<string> { testClassName }
                };
            });
        }
    }
}

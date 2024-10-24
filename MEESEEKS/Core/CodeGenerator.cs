using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MEESEEKS.Interfaces;
using MEESEEKS.Models;

namespace MEESEEKS.Core
{
    public class CodeGenerator : ICodeGenerator
    {
        public async Task<GeneratedCode> GenerateCodeAsync(CodeGenerationRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrEmpty(request.ClassName)) throw new ArgumentException("ClassName is required", nameof(request));
            if (string.IsNullOrEmpty(request.Namespace)) throw new ArgumentException("Namespace is required", nameof(request));

            var classDeclaration = await Task.Run(() =>
            {
                var declaration = SyntaxFactory.ClassDeclaration(request.ClassName)
                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));

                if (request.Interfaces?.Any() == true)
                {
                    declaration = declaration.AddBaseListTypes(
                        request.Interfaces.Select(i =>
                            SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(i))).ToArray());
                }

                var members = GenerateMembers(request);
                return declaration.AddMembers(members.ToArray());
            });

            var namespaceDeclaration = SyntaxFactory.NamespaceDeclaration(
                SyntaxFactory.ParseName(request.Namespace))
                .AddMembers(classDeclaration);

            var usings = GenerateUsingDirectives(request.Dependencies ?? new List<string>());
            var compilationUnit = SyntaxFactory.CompilationUnit()
                .AddUsings(usings.ToArray())
                .AddMembers(namespaceDeclaration);

            var code = await Task.Run(() => 
                compilationUnit.NormalizeWhitespace().ToFullString());

            return new GeneratedCode
            {
                ClassName = request.ClassName,
                Namespace = request.Namespace,
                Code = code,
                Dependencies = request.Dependencies ?? new List<string>(),
                Interfaces = request.Interfaces ?? new List<string>()
            };
        }

        public async Task<UnitTestCode> GenerateTestsAsync(GeneratedCode code)
        {
            if (code == null) throw new ArgumentNullException(nameof(code));
            if (string.IsNullOrEmpty(code.Code)) throw new ArgumentException("Code is required", nameof(code));

            var className = $"{code.ClassName}Tests";
            var testMethods = new List<MethodDeclarationSyntax>();

            var testClass = await Task.Run(() =>
            {
                var declaration = SyntaxFactory.ClassDeclaration(className)
                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));

                testMethods.AddRange(GenerateTestMethods(code));
                return declaration.AddMembers(testMethods.ToArray());
            });

            var namespaceDeclaration = SyntaxFactory.NamespaceDeclaration(
                SyntaxFactory.ParseName($"{code.Namespace}.Tests"))
                .AddMembers(testClass);

            var usings = GenerateTestUsingDirectives(code);
            var compilationUnit = SyntaxFactory.CompilationUnit()
                .AddUsings(usings.ToArray())
                .AddMembers(namespaceDeclaration);

            var testCode = await Task.Run(() => 
                compilationUnit.NormalizeWhitespace().ToFullString());

            return new UnitTestCode
            {
                TestClassName = className,
                TestCode = testCode,
                TestCases = testMethods.Select(m => m.Identifier.Text).ToList()
            };
        }

        public async Task<InterfaceDefinition> GenerateInterfaceAsync(GeneratedCode code)
        {
            if (code == null) throw new ArgumentNullException(nameof(code));
            if (string.IsNullOrEmpty(code.Code)) throw new ArgumentException("Code is required", nameof(code));

            var tree = CSharpSyntaxTree.ParseText(code.Code);
            var root = await tree.GetRootAsync();
            var classDeclaration = root.DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                .FirstOrDefault() ?? throw new InvalidOperationException("No class declaration found in the code");

            var methods = ExtractMethodDefinitions(classDeclaration);
            var properties = ExtractPropertyDefinitions(classDeclaration);

            return new InterfaceDefinition
            {
                InterfaceName = $"I{code.ClassName}",
                Namespace = code.Namespace,
                Methods = methods ?? new List<MethodDefinition>(),
                Properties = properties ?? new List<PropertyDefinition>()
            };
        }

        private IEnumerable<MemberDeclarationSyntax> GenerateMembers(CodeGenerationRequest request)
        {
            var members = new List<MemberDeclarationSyntax>();

            if (request.Methods != null)
            {
                foreach (var method in request.Methods)
                {
                    members.Add(GenerateMethodDeclaration(method));
                }
            }

            return members;
        }

        private MethodDeclarationSyntax GenerateMethodDeclaration(MethodDefinition method)
        {
            if (method == null) throw new ArgumentNullException(nameof(method));
            if (string.IsNullOrEmpty(method.Name)) throw new ArgumentException("Method name is required", nameof(method));
            if (string.IsNullOrEmpty(method.ReturnType)) throw new ArgumentException("Return type is required", nameof(method));

            var parameters = method.Parameters?.Select(p =>
                SyntaxFactory.Parameter(
                    SyntaxFactory.Identifier(p.Name))
                .WithType(SyntaxFactory.ParseTypeName(p.Type))) ?? Array.Empty<ParameterSyntax>();

            var returnType = SyntaxFactory.ParseTypeName(method.ReturnType);

            var methodDeclaration = SyntaxFactory.MethodDeclaration(returnType, method.Name)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddParameterListParameters(parameters.ToArray())
                .WithBody(SyntaxFactory.Block());

            if (method.Attributes?.Any() == true)
            {
                methodDeclaration = methodDeclaration.AddAttributeLists(
                    method.Attributes.Select(attr =>
                        SyntaxFactory.AttributeList(
                            SyntaxFactory.SingletonSeparatedList(
                                SyntaxFactory.Attribute(
                                    SyntaxFactory.IdentifierName(attr)))))
                    .ToArray());
            }

            return methodDeclaration;
        }

        private IEnumerable<MethodDeclarationSyntax> GenerateTestMethods(GeneratedCode code)
        {
            var methods = new List<MethodDeclarationSyntax>();
            var tree = CSharpSyntaxTree.ParseText(code.Code);
            var root = tree.GetRoot();
            var classDeclaration = root.DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                .FirstOrDefault();

            if (classDeclaration != null)
            {
                foreach (var method in classDeclaration.DescendantNodes().OfType<MethodDeclarationSyntax>())
                {
                    methods.Add(GenerateTestMethod(method));
                }
            }

            return methods;
        }

        private MethodDeclarationSyntax GenerateTestMethod(MethodDeclarationSyntax originalMethod)
        {
            var testMethodName = $"Test_{originalMethod.Identifier.Text}";
            
            return SyntaxFactory.MethodDeclaration(
                SyntaxFactory.PredefinedType(
                    SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                testMethodName)
                .AddAttributeLists(
                    SyntaxFactory.AttributeList(
                        SyntaxFactory.SingletonSeparatedList(
                            SyntaxFactory.Attribute(
                                SyntaxFactory.IdentifierName("Fact")))))
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .WithBody(SyntaxFactory.Block());
        }

        private IEnumerable<UsingDirectiveSyntax> GenerateUsingDirectives(List<string> dependencies)
        {
            var baseUsings = new[]
            {
                "System",
                "System.Collections.Generic",
                "System.Threading.Tasks"
            };

            return baseUsings.Concat(dependencies)
                .Select(d => SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(d)));
        }

        private IEnumerable<UsingDirectiveSyntax> GenerateTestUsingDirectives(GeneratedCode code)
        {
            var testUsings = new[]
            {
                "System",
                "System.Threading.Tasks",
                "Xunit",
                code.Namespace,
                "System.Collections.Generic"
            };

            return testUsings.Concat(code.Dependencies ?? Enumerable.Empty<string>())
                .Select(u => SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(u)));
        }

        private List<MethodDefinition> ExtractMethodDefinitions(ClassDeclarationSyntax classDeclaration)
        {
            if (classDeclaration == null) throw new ArgumentNullException(nameof(classDeclaration));

            return classDeclaration.DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .Select(static m => new MethodDefinition
                {
                    Name = m.Identifier.Text,
                    ReturnType = m.ReturnType.ToString(),
                    Parameters = m.ParameterList.Parameters.Select(static p => new ParameterDefinition
                    {
                        Name = p.Identifier.Text,
                        Type = p.Type?.ToString() ?? "object",
                        IsOptional = p.Default != null,
                        DefaultValue = p.Default?.Value?.ToString() ?? string.Empty
                    }).ToList() ?? new List<ParameterDefinition>(),
                    Attributes = m.AttributeLists
                        .SelectMany(al => al.Attributes)
                        .Select(a => a.Name.ToString())
                        .ToList() ?? new List<string>()
                }).ToList();
        }

        private List<PropertyDefinition> ExtractPropertyDefinitions(ClassDeclarationSyntax classDeclaration)
        {
            if (classDeclaration == null) throw new ArgumentNullException(nameof(classDeclaration));

            return classDeclaration.DescendantNodes()
                .OfType<PropertyDeclarationSyntax>()
                .Select(p => new PropertyDefinition
                {
                    Name = p.Identifier.Text,
                    Type = p.Type.ToString(),
                    HasGetter = p.AccessorList?.Accessors.Any(a => a.IsKind(SyntaxKind.GetAccessorDeclaration)) ?? false,
                    HasSetter = p.AccessorList?.Accessors.Any(a => a.IsKind(SyntaxKind.SetAccessorDeclaration)) ?? false,
                    Attributes = p.AttributeLists
                        .SelectMany(al => al.Attributes)
                        .Select(a => a.Name.ToString())
                        .ToList() ?? new List<string>()
                }).ToList();
        }
    }
}

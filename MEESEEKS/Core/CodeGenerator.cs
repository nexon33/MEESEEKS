using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MEESEEKS.Interfaces;
using MEESEEKS.Models.CodeGeneration;
using MEESEEKS.Models.Documentation;

namespace MEESEEKS.Core
{
    /// <summary>
    /// Provides functionality for generating C# code using the Roslyn compiler platform.
    /// This class supports generating modular and extensible code with interfaces, implementations,
    /// and unit tests, while maintaining clean architecture principles.
    /// </summary>
    public partial class CodeGenerator : ICodeGenerator
    {
        private const string Version = "1.0.0";

        /// <summary>
        /// Generates code based on the provided request, including interfaces, implementations,
        /// and unit tests if specified.
        /// </summary>
        /// <param name="request">The code generation request containing specifications.</param>
        /// <returns>A task that represents the asynchronous operation, containing the generated code.</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
        public async Task<GeneratedCode> GenerateCodeAsync(CodeGenerationRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var result = new GeneratedCode
            {
                GeneratorVersion = Version,
                GeneratedAt = DateTime.UtcNow
            };

            // Generate interface if requested
            if (request.GenerateInterfaces)
            {
                var interfaceFile = await GenerateInterfaceFileAsync(request);
                result.InterfaceFiles.Add(interfaceFile);
            }

            // Generate implementation
            var implementationFile = await GenerateImplementationFileAsync(request);
            result.SourceFiles.Add(implementationFile);

            // Generate tests if requested
            if (request.GenerateTests)
            {
                var testFile = await GenerateTestFileAsync(request, implementationFile);
                result.TestFiles.Add(testFile);
            }

            // Generate documentation if requested
            if (request.GenerateDocumentation)
            {
                result.Documentation = await GenerateDocumentationAsync(request, result);
            }

            return result;
        }

        /// <summary>
        /// Generates unit tests for the specified code.
        /// </summary>
        /// <param name="code">The generated code for which to create tests.</param>
        /// <returns>A task that represents the asynchronous operation, containing the generated test code.</returns>
        /// <exception cref="ArgumentNullException">Thrown when code is null.</exception>
        public async Task<UnitTestCode> GenerateTestsAsync(GeneratedCode code)
        {
            if (code == null) throw new ArgumentNullException(nameof(code));

            var sourceFile = code.SourceFiles.FirstOrDefault() 
                ?? throw new ArgumentException("No source files found in generated code", nameof(code));

            var className = sourceFile.DefinedClasses.FirstOrDefault()
                ?? throw new ArgumentException("No classes found in source file", nameof(code));

            var testClassName = $"{className}Tests";
            var testNamespace = $"{sourceFile.Namespace}.Tests";

            var testMethods = await GenerateTestMethodsAsync(sourceFile);
            var setupCode = await GenerateTestSetupAsync(sourceFile);
            var testSourceCode = await GenerateTestSourceCodeAsync(testClassName, testNamespace, testMethods, setupCode);

            return new UnitTestCode
            {
                SourceCode = testSourceCode,
                TestClassName = testClassName,
                Namespace = testNamespace,
                TestMethods = testMethods.Select(m => m.Identifier.Text).ToList(),
                SetupCode = setupCode?.ToString(),
                TestDependencies = new List<DependencyInfo>
                {
                    new DependencyInfo { Name = "xunit", Version = "2.4.1" },
                    new DependencyInfo { Name = "xunit.runner.visualstudio", Version = "2.4.3" }
                }
            };
        }

        /// <summary>
        /// Extracts interface definitions from the generated code.
        /// </summary>
        /// <param name="code">The generated code from which to extract interfaces.</param>
        /// <returns>A task that represents the asynchronous operation, containing the interface definition.</returns>
        /// <exception cref="ArgumentNullException">Thrown when code is null.</exception>
        public async Task<InterfaceDefinition> GenerateInterfaceAsync(GeneratedCode code)
        {
            if (code == null) throw new ArgumentNullException(nameof(code));

            var sourceFile = code.SourceFiles.FirstOrDefault() 
                ?? throw new ArgumentException("No source files found in generated code", nameof(code));

            var tree = CSharpSyntaxTree.ParseText(sourceFile.Content);
            var root = await tree.GetRootAsync();
            var classDeclaration = root.DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                .FirstOrDefault()
                ?? throw new InvalidOperationException("No class declaration found in the code");

            var interfaceName = $"I{sourceFile.DefinedClasses[

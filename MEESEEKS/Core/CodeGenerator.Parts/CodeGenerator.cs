using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using MEESEEKS.Core.CodeGeneration;
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
        private readonly CodeGeneratorCore _codeGeneratorCore;
        private readonly Workspace _workspace;

        /// <summary>
        /// Initializes a new instance of the CodeGenerator class.
        /// </summary>
        /// <param name="workspace">The Roslyn workspace to use for code generation.</param>
        /// <exception cref="ArgumentNullException">Thrown when workspace is null.</exception>
        public CodeGenerator(Workspace workspace)
        {
            _workspace = workspace ?? throw new ArgumentNullException(nameof(workspace));
            _codeGeneratorCore = new CodeGeneratorCore();
        }

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
                GeneratedAt = DateTime.UtcNow,
                SourceFiles = new List<SourceFile>(),
                InterfaceFiles = new List<SourceFile>(),
                TestFiles = new List<SourceFile>()
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

            return result;
        }

        /// <summary>
        /// Generates unit tests for the specified generated code.
        /// </summary>
        /// <param name="code">The generated code to create tests for.</param>
        /// <returns>A task representing the asynchronous operation that returns the generated unit test code.</returns>
        /// <exception cref="ArgumentNullException">Thrown when code is null.</exception>
        public async Task<UnitTestCode> GenerateTestsAsync(GeneratedCode code)
        {
            if (code == null) throw new ArgumentNullException(nameof(code));

            return await _codeGeneratorCore.GenerateTestsAsync(code);
        }

        /// <summary>
        /// Generates an interface definition based on the specified generated code.
        /// </summary>
        /// <param name="code">The generated code to create an interface for.</param>
        /// <returns>A task representing the asynchronous operation that returns the generated interface definition.</returns>
        /// <exception cref="ArgumentNullException">Thrown when code is null.</exception>
        public async Task<InterfaceDefinition> GenerateInterfaceAsync(GeneratedCode code)
        {
            if (code == null) throw new ArgumentNullException(nameof(code));

            return await _codeGeneratorCore.GenerateInterfaceAsync(code);
        }

        /// <summary>
        /// Generates a test file for the specified implementation.
        /// </summary>
        /// <param name="request">The code generation request containing specifications.</param>
        /// <param name="implementationFile">The implementation file to create tests for.</param>
        /// <returns>A task representing the asynchronous operation that returns the generated test file.</returns>
        /// <exception cref="ArgumentNullException">Thrown when request or implementationFile is null.</exception>
        private async Task<SourceFile> GenerateTestFileAsync(CodeGenerationRequest request, SourceFile implementationFile)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (implementationFile == null) throw new ArgumentNullException(nameof(implementationFile));

            return await _codeGeneratorCore.GenerateTestFileAsync(request, implementationFile);
        }
    }
}

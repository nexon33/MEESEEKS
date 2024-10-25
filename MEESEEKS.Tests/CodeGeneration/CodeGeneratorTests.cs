using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using MEESEEKS.Core;
using MEESEEKS.Models.CodeGeneration;
using Xunit;

namespace MEESEEKS.Tests.CodeGeneration
{
    /// <summary>
    /// Tests for the CodeGenerator class.
    /// </summary>
    public class CodeGeneratorTests
    {
        private readonly Workspace _workspace;
        private readonly CodeGenerator _codeGenerator;

        /// <summary>
        /// Initializes a new instance of the CodeGeneratorTests class.
        /// </summary>
        public CodeGeneratorTests()
        {
            _workspace = new AdhocWorkspace();
            _codeGenerator = new CodeGenerator(_workspace);
        }

        /// <summary>
        /// Tests that GenerateCodeAsync generates interface, implementation, and test files when requested.
        /// </summary>
        [Fact]
        public async Task GenerateCodeAsync_GeneratesAllRequestedFiles()
        {
            // Arrange
            var request = new CodeGenerationRequest
            {
                Description = "Calculator",
                Language = "MEESEEKS.Examples",
                GenerateInterfaces = true,
                GenerateTests = true
            };

            // Act
            var result = await _codeGenerator.GenerateCodeAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.InterfaceFiles);
            Assert.NotEmpty(result.SourceFiles);
            Assert.NotEmpty(result.TestFiles);
        }

        /// <summary>
        /// Tests that GenerateTestsAsync generates test code for the provided generated code.
        /// </summary>
        [Fact]
        public async Task GenerateTestsAsync_GeneratesTestCode()
        {
            // Arrange
            var generatedCode = new GeneratedCode
            {
                SourceFiles = new List<SourceFile>
                {
                    new SourceFile
                    {
                        FilePath = "Calculator.cs",
                        Content = "public class Calculator {}",
                        FileType = SourceFileType.Class,
                        Language = "MEESEEKS.Examples",
                        Namespace = "MEESEEKS.Examples"
                    }
                }
            };

            // Act
            var result = await _codeGenerator.GenerateTestsAsync(generatedCode);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.SourceCode);
            Assert.Contains("CalculatorTests", result.TestClassName);
        }

        /// <summary>
        /// Tests that GenerateInterfaceAsync generates interface code for the provided generated code.
        /// </summary>
        [Fact]
        public async Task GenerateInterfaceAsync_GeneratesInterfaceCode()
        {
            // Arrange
            var generatedCode = new GeneratedCode
            {
                SourceFiles = new List<SourceFile>
                {
                    new SourceFile
                    {
                        FilePath = "Calculator.cs",
                        Content = "public class Calculator {}",
                        FileType = SourceFileType.Class,
                        Language = "MEESEEKS.Examples",
                        Namespace = "MEESEEKS.Examples"
                    }
                }
            };

            // Act
            var result = await _codeGenerator.GenerateInterfaceAsync(generatedCode);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.SourceCode);
            Assert.Contains("ICalculator", result.InterfaceName);
        }
    }
}

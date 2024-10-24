using System;
using System.Threading.Tasks;
using MEESEEKS.Core;
using MEESEEKS.Models.CodeAnalysis;
using Xunit;

namespace MEESEEKS.Tests.Analysis
{
    /// <summary>
    /// Basic functionality tests for the CodeAnalyzer class.
    /// </summary>
    public class CodeAnalyzerBasicTests
    {
        private readonly CodeAnalyzer _analyzer;

        public CodeAnalyzerBasicTests()
        {
            _analyzer = new CodeAnalyzer();
        }

        [Fact]
        public async Task AnalyzeCodeAsync_WithValidCode_ReturnsAnalysisResult()
        {
            // Arrange
            var code = "public class SimpleClass { }";

            // Act
            var result = await _analyzer.AnalyzeCodeAsync(code);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Issues);
            Assert.NotNull(result.Metrics);
            Assert.NotNull(result.Context);
        }

        [Fact]
        public async Task AnalyzeCodeAsync_WithEmptyCode_ThrowsArgumentException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _analyzer.AnalyzeCodeAsync(string.Empty));
        }

        [Fact]
        public async Task GenerateCodeModificationAsync_WithNullAnalysis_ThrowsArgumentNullException()
        {
            // Act & Assert
            CodeAnalysisResult? nullAnalysis = null;
            await Assert.ThrowsAsync<ArgumentNullException>(() => _analyzer.GenerateCodeModificationAsync(nullAnalysis!));
        }
    }
}

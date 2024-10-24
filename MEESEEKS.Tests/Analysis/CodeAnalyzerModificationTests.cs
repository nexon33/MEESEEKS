using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MEESEEKS.Core;
using MEESEEKS.Models.CodeAnalysis;
using Xunit;

namespace MEESEEKS.Tests.Analysis
{
    /// <summary>
    /// Tests for code modification functionality.
    /// </summary>
    public class CodeAnalyzerModificationTests
    {
        private readonly CodeAnalyzer _analyzer;

        public CodeAnalyzerModificationTests()
        {
            _analyzer = new CodeAnalyzer();
        }

        [Fact]
        public async Task GenerateCodeModificationAsync_WithValidAnalysis_ReturnsModification()
        {
            // Arrange
            var analysis = new CodeAnalysisResult
            {
                FilePath = "test.cs",
                Issues = new List<CodeIssue>(),
                Metrics = new CodeMetrics(),
                Context = new CodeContext()
            };

            // Act
            var result = await _analyzer.GenerateCodeModificationAsync(analysis);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("test.cs", result.FilePath);
            Assert.NotNull(result.Changes);
            Assert.NotEqual(default, result.ModifiedAt);
        }

        [Fact]
        public async Task GenerateCodeModificationAsync_PreservesOriginalFilePath()
        {
            // Arrange
            var filePath = "/path/to/source.cs";
            var analysis = new CodeAnalysisResult
            {
                FilePath = filePath,
                Issues = new List<CodeIssue>(),
                Metrics = new CodeMetrics(),
                Context = new CodeContext()
            };

            // Act
            var result = await _analyzer.GenerateCodeModificationAsync(analysis);

            // Assert
            Assert.Equal(filePath, result.FilePath);
        }

        [Fact]
        public async Task GenerateCodeModificationAsync_InitializesChangesCollection()
        {
            // Arrange
            var analysis = new CodeAnalysisResult
            {
                FilePath = "test.cs",
                Issues = new List<CodeIssue>(),
                Metrics = new CodeMetrics(),
                Context = new CodeContext()
            };

            // Act
            var result = await _analyzer.GenerateCodeModificationAsync(analysis);

            // Assert
            Assert.NotNull(result.Changes);
            Assert.Empty(result.Changes);
        }

        [Fact]
        public async Task GenerateCodeModificationAsync_SetsModificationTimestamp()
        {
            // Arrange
            var analysis = new CodeAnalysisResult
            {
                FilePath = "test.cs",
                Issues = new List<CodeIssue>(),
                Metrics = new CodeMetrics(),
                Context = new CodeContext()
            };

            var beforeTime = DateTime.UtcNow;

            // Act
            var result = await _analyzer.GenerateCodeModificationAsync(analysis);

            // Assert
            Assert.True(result.ModifiedAt >= beforeTime);
            Assert.True(result.ModifiedAt <= DateTime.UtcNow);
        }
    }
}

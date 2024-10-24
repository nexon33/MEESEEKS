using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MEESEEKS.Core;
using Xunit;

namespace MEESEEKS.Tests.Analysis
{
    /// <summary>
    /// Tests for code context extraction functionality.
    /// </summary>
    public class CodeAnalyzerContextTests
    {
        private readonly CodeAnalyzer _analyzer;

        public CodeAnalyzerContextTests()
        {
            _analyzer = new CodeAnalyzer();
        }

        [Fact]
        public async Task AnalyzeCodeAsync_ExtractsClassDefinitions()
        {
            // Arrange
            var code = @"
                namespace TestNamespace
                {
                    public class Class1 { }
                    internal class Class2 { }
                }";

            // Act
            var result = await _analyzer.AnalyzeCodeAsync(code);

            // Assert
            Assert.NotNull(result.Context);
            var contextDict = result.Context.ToDictionary();
            Assert.NotNull(contextDict);
            Assert.True(contextDict.ContainsKey("Classes"));
            var classes = (contextDict["Classes"] as IEnumerable<string>) ?? Array.Empty<string>();
            Assert.Contains(classes, cls => cls == "Class1");
            Assert.Contains(classes, cls => cls == "Class2");
        }

        [Fact]
        public async Task AnalyzeCodeAsync_ExtractsMethodDefinitions()
        {
            // Arrange
            var code = @"
                public class TestClass
                {
                    public void Method1() { }
                    private int Method2() => 42;
                }";

            // Act
            var result = await _analyzer.AnalyzeCodeAsync(code);

            // Assert
            Assert.NotNull(result.Context);
            var contextDict = result.Context.ToDictionary();
            Assert.NotNull(contextDict);
            Assert.True(contextDict.ContainsKey("Methods"));
            var methods = (contextDict["Methods"] as IEnumerable<string>) ?? Array.Empty<string>();
            Assert.Contains(methods, m => m == "Method1");
            Assert.Contains(methods, m => m == "Method2");
        }

        [Fact]
        public async Task AnalyzeCodeAsync_ExtractsUsingDirectives()
        {
            // Arrange
            var code = @"
                using System;
                using System.Linq;
                using System.Collections.Generic;

                public class TestClass { }";

            // Act
            var result = await _analyzer.AnalyzeCodeAsync(code);

            // Assert
            Assert.NotNull(result.Context);
            var contextDict = result.Context.ToDictionary();
            Assert.NotNull(contextDict);
            Assert.True(contextDict.ContainsKey("Usings"));
            var usings = (contextDict["Usings"] as IEnumerable<string>) ?? Array.Empty<string>();
            Assert.Contains(usings, u => u == "System");
            Assert.Contains(usings, u => u == "System.Linq");
            Assert.Contains(usings, u => u == "System.Collections.Generic");
        }
    }
}

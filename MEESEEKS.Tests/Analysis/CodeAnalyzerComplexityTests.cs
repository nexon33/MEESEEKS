using System.Linq;
using System.Threading.Tasks;
using MEESEEKS.Core;
using Xunit;

namespace MEESEEKS.Tests.Analysis
{
    /// <summary>
    /// Tests for code complexity analysis functionality.
    /// </summary>
    public class CodeAnalyzerComplexityTests
    {
        private readonly CodeAnalyzer _analyzer;

        public CodeAnalyzerComplexityTests()
        {
            _analyzer = new CodeAnalyzer();
        }

        [Fact]
        public async Task AnalyzeCodeAsync_ComplexityMetricsAreCalculated()
        {
            // Arrange
            var code = @"
                public class ComplexClass
                {
                    public int CalculateComplexity(int x)
                    {
                        if (x > 0)
                        {
                            if (x < 10)
                            {
                                return x * 2;
                            }
                            else if (x < 20)
                            {
                                return x * 3;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                x += i;
                            }
                        }
                        return x;
                    }
                }";

            // Act
            var result = await _analyzer.AnalyzeCodeAsync(code);

            // Assert
            Assert.NotNull(result.Metrics);
            Assert.NotEmpty(result.Metrics.Metrics);
            var complexityMetric = result.Metrics.Metrics.FirstOrDefault(m => m.Name.Contains("Cyclomatic Complexity"));
            Assert.NotNull(complexityMetric);
            Assert.True(complexityMetric.Value > 1);
        }

        [Fact]
        public async Task AnalyzeCodeAsync_SimpleMethod_HasBaseComplexity()
        {
            // Arrange
            var code = @"
                public class SimpleClass
                {
                    public void SimpleMethod()
                    {
                        Console.WriteLine(""Hello"");
                    }
                }";

            // Act
            var result = await _analyzer.AnalyzeCodeAsync(code);

            // Assert
            var complexityMetric = result.Metrics.Metrics.FirstOrDefault(m => m.Name.Contains("Cyclomatic Complexity"));
            Assert.NotNull(complexityMetric);
            Assert.Equal(1, complexityMetric.Value);
        }
    }
}

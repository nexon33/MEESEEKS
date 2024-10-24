using System.Threading.Tasks;
using MEESEEKS.Models.CodeAnalysis;

namespace MEESEEKS.Interfaces
{
    /// <summary>
    /// Provides functionality for analyzing and modifying code.
    /// </summary>
    public interface ICodeAnalyzer
    {
        /// <summary>
        /// Analyzes the provided code content and generates a detailed analysis result.
        /// </summary>
        /// <param name="codeContent">The source code content to analyze.</param>
        /// <returns>A task representing the asynchronous operation that returns a CodeAnalysisResult containing the analysis findings.</returns>
        Task<CodeAnalysisResult> AnalyzeCodeAsync(string codeContent);

        /// <summary>
        /// Generates code modifications based on the provided code analysis result.
        /// </summary>
        /// <param name="analysis">The code analysis result containing the findings to base modifications on.</param>
        /// <returns>A task representing the asynchronous operation that returns a CodeModification containing the suggested code changes.</returns>
        Task<CodeModification> GenerateCodeModificationAsync(CodeAnalysisResult analysis);
    }
}

using System.Threading.Tasks;
using MEESEEKS.Models.CodeGeneration;

namespace MEESEEKS.Interfaces
{
    /// <summary>
    /// Provides functionality for generating code, tests, and interfaces.
    /// </summary>
    public interface ICodeGenerator
    {
        /// <summary>
        /// Generates code based on the specified code generation request.
        /// </summary>
        /// <param name="request">The request containing parameters and requirements for code generation.</param>
        /// <returns>A task representing the asynchronous operation that returns the GeneratedCode.</returns>
        Task<GeneratedCode> GenerateCodeAsync(CodeGenerationRequest request);

        /// <summary>
        /// Generates unit tests for the specified generated code.
        /// </summary>
        /// <param name="code">The generated code to create tests for.</param>
        /// <returns>A task representing the asynchronous operation that returns the generated unit test code.</returns>
        Task<UnitTestCode> GenerateTestsAsync(GeneratedCode code);

        /// <summary>
        /// Generates an interface definition based on the specified generated code.
        /// </summary>
        /// <param name="code">The generated code to create an interface for.</param>
        /// <returns>A task representing the asynchronous operation that returns the generated interface definition.</returns>
        Task<InterfaceDefinition> GenerateInterfaceAsync(GeneratedCode code);
    }
}

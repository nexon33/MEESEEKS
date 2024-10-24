using System.Threading.Tasks;

namespace MEESEEKS.Interfaces
{
    /// <summary>
    /// Provides functionality for managing .NET solutions and projects.
    /// </summary>
    public interface ISolutionManager
    {
        /// <summary>
        /// Loads a .NET solution from the specified path.
        /// </summary>
        /// <param name="solutionPath">The path to the .NET solution file.</param>
        /// <returns>A task representing the asynchronous load operation.</returns>
        Task LoadSolutionAsync(string solutionPath);

        /// <summary>
        /// Adds a new project to the loaded solution.
        /// </summary>
        /// <param name="projectName">The name of the project to add.</param>
        /// <param name="projectType">The type of project to create (e.g., "classlib", "console").</param>
        /// <returns>A task representing the asynchronous project creation operation.</returns>
        Task AddProjectAsync(string projectName, string projectType);

        /// <summary>
        /// Adds a file to a project in the solution.
        /// </summary>
        /// <param name="projectName">The name of the project to add the file to.</param>
        /// <param name="filePath">The path where the file should be created.</param>
        /// <param name="content">The content of the file.</param>
        /// <returns>A task representing the asynchronous file creation operation.</returns>
        Task AddFileToProjectAsync(string projectName, string filePath, string content);

        /// <summary>
        /// Builds the entire solution.
        /// </summary>
        /// <returns>A task representing the asynchronous build operation.</returns>
        Task BuildSolutionAsync();

        /// <summary>
        /// Runs all tests in the solution.
        /// </summary>
        /// <returns>A task representing the asynchronous test execution operation.</returns>
        Task RunTestsAsync();
    }
}

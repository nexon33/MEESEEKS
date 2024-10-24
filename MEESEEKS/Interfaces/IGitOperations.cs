using System.Threading.Tasks;

namespace MEESEEKS.Interfaces
{
    /// <summary>
    /// Provides functionality for performing Git version control operations.
    /// </summary>
    public interface IGitOperations
    {
        /// <summary>
        /// Clones a Git repository from the specified URL to the target path.
        /// </summary>
        /// <param name="repoUrl">The URL of the Git repository to clone.</param>
        /// <param name="targetPath">The local path where the repository should be cloned.</param>
        /// <returns>A task representing the asynchronous clone operation.</returns>
        Task CloneRepositoryAsync(string repoUrl, string targetPath);

        /// <summary>
        /// Commits changes to the Git repository with the specified commit message.
        /// </summary>
        /// <param name="message">The commit message describing the changes.</param>
        /// <returns>A task representing the asynchronous commit operation.</returns>
        Task CommitChangesAsync(string message);

        /// <summary>
        /// Pushes committed changes to the remote repository.
        /// </summary>
        /// <returns>A task representing the asynchronous push operation.</returns>
        Task PushChangesAsync();

        /// <summary>
        /// Pulls the latest changes from the remote repository.
        /// </summary>
        /// <returns>A task representing the asynchronous pull operation.</returns>
        Task PullLatestAsync();
    }
}

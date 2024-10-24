namespace MEESEEKS.Models.Git
{
    /// <summary>
    /// Represents a Git commit author or committer.
    /// </summary>
    public class GitAuthor
    {
        /// <summary>
        /// Name of the author.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Email of the author.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Username in the Git hosting service (if available).
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// GPG key ID if the commit is signed.
        /// </summary>
        public string GpgKeyId { get; set; }
    }
}

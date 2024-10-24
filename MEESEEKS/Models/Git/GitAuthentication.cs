namespace MEESEEKS.Models.Git
{
    /// <summary>
    /// Represents authentication configuration for Git operations.
    /// </summary>
    public class GitAuthentication
    {
        /// <summary>
        /// Username for authentication.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Personal access token or password.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Path to SSH key file if using SSH authentication.
        /// </summary>
        public string SshKeyPath { get; set; }

        /// <summary>
        /// Type of authentication (e.g., "https", "ssh").
        /// </summary>
        public string AuthType { get; set; }

        /// <summary>
        /// Whether to store credentials in credential manager.
        /// </summary>
        public bool StoreCredentials { get; set; }
    }
}

namespace MEESEEKS.Models.Git
{
    /// <summary>
    /// Represents a Git remote configuration.
    /// </summary>
    public class GitRemote
    {
        /// <summary>
        /// Name of the remote.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// URL of the remote.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Whether this is a fetch remote.
        /// </summary>
        public bool IsFetchRemote { get; set; }

        /// <summary>
        /// Whether this is a push remote.
        /// </summary>
        public bool IsPushRemote { get; set; }

        /// <summary>
        /// Mirror configuration if this is a mirror remote.
        /// </summary>
        public bool IsMirror { get; set; }
    }
}

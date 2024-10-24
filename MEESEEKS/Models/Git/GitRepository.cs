using System;

namespace MEESEEKS.Models.Git
{
    /// <summary>
    /// Represents a Git repository that a MEESEEKS agent works with.
    /// </summary>
    public class GitRepository
    {
        /// <summary>
        /// URL of the Git repository.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Local path where the repository is cloned.
        /// </summary>
        public string LocalPath { get; set; }

        /// <summary>
        /// Current branch name.
        /// </summary>
        public string CurrentBranch { get; set; }

        /// <summary>
        /// Latest commit hash.
        /// </summary>
        public string LastCommitHash { get; set; }

        /// <summary>
        /// Whether there are uncommitted changes.
        /// </summary>
        public bool HasUncommittedChanges { get; set; }

        /// <summary>
        /// Authentication configuration for the repository.
        /// </summary>
        public GitAuthentication Authentication { get; set; }

        /// <summary>
        /// Remote configuration settings.
        /// </summary>
        public GitRemoteConfig RemoteConfig { get; set; }

        /// <summary>
        /// Time of last synchronization with remote.
        /// </summary>
        public DateTime LastSyncTime { get; set; }
    }
}

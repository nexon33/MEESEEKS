using System.Collections.Generic;

namespace MEESEEKS.Models.Git
{
    /// <summary>
    /// Represents configuration for Git remotes.
    /// </summary>
    public class GitRemoteConfig
    {
        /// <summary>
        /// Name of the primary remote (e.g., "origin").
        /// </summary>
        public string PrimaryRemoteName { get; set; }

        /// <summary>
        /// URL of the primary remote.
        /// </summary>
        public string PrimaryRemoteUrl { get; set; }

        /// <summary>
        /// Additional remote configurations.
        /// </summary>
        public List<GitRemote> AdditionalRemotes { get; set; } = new List<GitRemote>();

        /// <summary>
        /// Branch tracking configurations.
        /// </summary>
        public Dictionary<string, string> BranchTracking { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Push configurations.
        /// </summary>
        public GitPushConfig PushConfig { get; set; }
    }
}

namespace MEESEEKS.Models.Git
{
    /// <summary>
    /// Represents Git push configuration settings.
    /// </summary>
    public class GitPushConfig
    {
        /// <summary>
        /// Default remote to push to.
        /// </summary>
        public string DefaultRemote { get; set; }

        /// <summary>
        /// Whether to automatically push tags.
        /// </summary>
        public bool PushTags { get; set; }

        /// <summary>
        /// Whether to force push.
        /// </summary>
        public bool ForcePush { get; set; }

        /// <summary>
        /// Whether to push all branches.
        /// </summary>
        public bool PushAllBranches { get; set; }

        /// <summary>
        /// Whether to follow tags.
        /// </summary>
        public bool FollowTags { get; set; }
    }
}

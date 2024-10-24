using System;
using System.Collections.Generic;

namespace MEESEEKS.Models.Git
{
    /// <summary>
    /// Represents a Git commit.
    /// </summary>
    public class GitCommit
    {
        /// <summary>
        /// Commit hash.
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// Short version of the commit hash.
        /// </summary>
        public string ShortHash { get; set; }

        /// <summary>
        /// Commit message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Author of the commit.
        /// </summary>
        public GitAuthor Author { get; set; }

        /// <summary>
        /// Committer of the commit (if different from author).
        /// </summary>
        public GitAuthor Committer { get; set; }

        /// <summary>
        /// Time when the commit was authored.
        /// </summary>
        public DateTime AuthorDate { get; set; }

        /// <summary>
        /// Time when the commit was committed.
        /// </summary>
        public DateTime CommitDate { get; set; }

        /// <summary>
        /// Parent commit hashes.
        /// </summary>
        public List<string> ParentHashes { get; set; } = new List<string>();

        /// <summary>
        /// Files changed in this commit.
        /// </summary>
        public List<GitFileChange> ChangedFiles { get; set; } = new List<GitFileChange>();

        /// <summary>
        /// Whether this is a merge commit.
        /// </summary>
        public bool IsMergeCommit => ParentHashes.Count > 1;

        /// <summary>
        /// Tags associated with this commit.
        /// </summary>
        public List<string> Tags { get; set; } = new List<string>();

        /// <summary>
        /// Number of additions in this commit.
        /// </summary>
        public int Additions { get; set; }

        /// <summary>
        /// Number of deletions in this commit.
        /// </summary>
        public int Deletions { get; set; }
    }
}

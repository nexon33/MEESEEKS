namespace MEESEEKS.Models.Git.Enums
{
    /// <summary>
    /// Types of changes that can occur in Git.
    /// </summary>
    public enum GitChangeType
    {
        /// <summary>
        /// File was added.
        /// </summary>
        Added,

        /// <summary>
        /// File was modified.
        /// </summary>
        Modified,

        /// <summary>
        /// File was deleted.
        /// </summary>
        Deleted,

        /// <summary>
        /// File was renamed.
        /// </summary>
        Renamed,

        /// <summary>
        /// File mode was changed.
        /// </summary>
        ModeChanged
    }
}

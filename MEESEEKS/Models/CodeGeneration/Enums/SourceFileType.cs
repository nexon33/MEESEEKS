namespace MEESEEKS.Models.CodeGeneration.Enums
{
    /// <summary>
    /// Types of source files that can be generated.
    /// </summary>
    public enum SourceFileType
    {
        /// <summary>
        /// Class implementation file.
        /// </summary>
        Class,

        /// <summary>
        /// Interface definition file.
        /// </summary>
        Interface,

        /// <summary>
        /// Unit test file.
        /// </summary>
        Test,

        /// <summary>
        /// Configuration file.
        /// </summary>
        Config,

        /// <summary>
        /// Documentation file.
        /// </summary>
        Documentation
    }
}

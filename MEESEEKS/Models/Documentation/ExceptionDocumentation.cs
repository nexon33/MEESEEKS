namespace MEESEEKS.Models.Documentation
{
    /// <summary>
    /// Represents documentation for an exception.
    /// </summary>
    public class ExceptionDocumentation
    {
        /// <summary>
        /// Type of the exception.
        /// </summary>
        public string ExceptionType { get; set; }

        /// <summary>
        /// Conditions under which this exception is thrown.
        /// </summary>
        public string Conditions { get; set; }

        /// <summary>
        /// How to handle or prevent this exception.
        /// </summary>
        public string Mitigation { get; set; }
    }
}

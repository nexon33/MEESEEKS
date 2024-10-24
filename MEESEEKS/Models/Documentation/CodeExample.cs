namespace MEESEEKS.Models.Documentation
{
    /// <summary>
    /// Represents a code example with explanation.
    /// </summary>
    public class CodeExample
    {
        /// <summary>
        /// Title or description of the example.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The example code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Explanation of how the example works.
        /// </summary>
        public string Explanation { get; set; }

        /// <summary>
        /// Expected output or result.
        /// </summary>
        public string ExpectedOutput { get; set; }

        /// <summary>
        /// Notes or additional context.
        /// </summary>
        public string Notes { get; set; }
    }
}

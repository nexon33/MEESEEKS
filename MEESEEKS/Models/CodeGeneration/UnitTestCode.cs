using System.Collections.Generic;

namespace MEESEEKS.Models.CodeGeneration
{
    /// <summary>
    /// Represents generated unit test code for a specific implementation.
    /// </summary>
    public class UnitTestCode
    {
        /// <summary>
        /// Gets or sets the source code of the generated unit tests.
        /// </summary>
        public required string SourceCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the test class.
        /// </summary>
        public required string TestClassName { get; set; }

        /// <summary>
        /// Gets or sets the namespace for the test class.
        /// </summary>
        public required string Namespace { get; set; }

        /// <summary>
        /// Gets or sets the list of test method names.
        /// </summary>
        public List<string> TestMethods { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the list of required test dependencies.
        /// </summary>
        public List<DependencyInfo> TestDependencies { get; set; } = new List<DependencyInfo>();

        /// <summary>
        /// Gets or sets any test setup code required for the tests.
        /// </summary>
        public string? SetupCode { get; set; }

        /// <summary>
        /// Gets or sets any test cleanup code required for the tests.
        /// </summary>
        public string? CleanupCode { get; set; }

        /// <summary>
        /// Gets or sets any test fixtures or shared test data.
        /// </summary>
        public Dictionary<string, object> TestFixtures { get; set; } = new Dictionary<string, object>();
    }
}

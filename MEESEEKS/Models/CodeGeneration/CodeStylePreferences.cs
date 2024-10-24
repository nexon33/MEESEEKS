namespace MEESEEKS.Models.CodeGeneration
{
    /// <summary>
    /// Represents code style preferences for generated code.
    /// </summary>
    public class CodeStylePreferences
    {
        /// <summary>
        /// Indentation style (spaces or tabs).
        /// </summary>
        public IndentationStyle IndentStyle { get; set; }

        /// <summary>
        /// Number of spaces for indentation if using spaces.
        /// </summary>
        public int IndentSize { get; set; }

        /// <summary>
        /// Maximum line length before wrapping.
        /// </summary>
        public int MaxLineLength { get; set; }

        /// <summary>
        /// Preferred naming convention for variables.
        /// </summary>
        public NamingConvention VariableNaming { get; set; }

        /// <summary>
        /// Preferred naming convention for methods.
        /// </summary>
        public NamingConvention MethodNaming { get; set; }

        /// <summary>
        /// Preferred naming convention for classes.
        /// </summary>
        public NamingConvention ClassNaming { get; set; }

        /// <summary>
        /// Whether to use var keyword in C#.
        /// </summary>
        public bool UseVarKeyword { get; set; }

        /// <summary>
        /// Whether to use expression-bodied members.
        /// </summary>
        public bool UseExpressionBodiedMembers { get; set; }
    }

    /// <summary>
    /// Represents indentation style options.
    /// </summary>
    public enum IndentationStyle
    {
        /// <summary>
        /// Use spaces for indentation.
        /// </summary>
        Spaces,

        /// <summary>
        /// Use tabs for indentation.
        /// </summary>
        Tabs
    }

    /// <summary>
    /// Represents naming convention options.
    /// </summary>
    public enum NamingConvention
    {
        /// <summary>
        /// camelCase naming.
        /// </summary>
        CamelCase,

        /// <summary>
        /// PascalCase naming.
        /// </summary>
        PascalCase,

        /// <summary>
        /// snake_case naming.
        /// </summary>
        SnakeCase,

        /// <summary>
        /// kebab-case naming.
        /// </summary>
        KebabCase
    }
}

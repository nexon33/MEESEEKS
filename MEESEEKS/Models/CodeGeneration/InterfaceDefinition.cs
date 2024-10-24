using System.Collections.Generic;

namespace MEESEEKS.Models.CodeGeneration
{
    /// <summary>
    /// Represents a generated interface definition.
    /// </summary>
    public class InterfaceDefinition
    {
        /// <summary>
        /// Gets or sets the source code of the generated interface.
        /// </summary>
        public required string SourceCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the interface.
        /// </summary>
        public required string InterfaceName { get; set; }

        /// <summary>
        /// Gets or sets the namespace for the interface.
        /// </summary>
        public required string Namespace { get; set; }

        /// <summary>
        /// Gets or sets the list of method definitions in the interface.
        /// </summary>
        public List<MethodDefinition> Methods { get; set; } = new List<MethodDefinition>();

        /// <summary>
        /// Gets or sets the list of property definitions in the interface.
        /// </summary>
        public List<PropertyDefinition> Properties { get; set; } = new List<PropertyDefinition>();

        /// <summary>
        /// Gets or sets the list of interfaces this interface extends.
        /// </summary>
        public List<string> ExtendedInterfaces { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the XML documentation for the interface.
        /// </summary>
        public string? Documentation { get; set; }

        /// <summary>
        /// Gets or sets any generic type parameters for the interface.
        /// </summary>
        public List<GenericTypeParameter> GenericParameters { get; set; } = new List<GenericTypeParameter>();
    }

    /// <summary>
    /// Represents a method definition in an interface.
    /// </summary>
    public class MethodDefinition
    {
        /// <summary>
        /// Gets or sets the name of the method.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the return type of the method.
        /// </summary>
        public required string ReturnType { get; set; }

        /// <summary>
        /// Gets or sets the list of parameters for the method.
        /// </summary>
        public List<ParameterDefinition> Parameters { get; set; } = new List<ParameterDefinition>();

        /// <summary>
        /// Gets or sets the XML documentation for the method.
        /// </summary>
        public string? Documentation { get; set; }

        /// <summary>
        /// Gets or sets any generic type parameters for the method.
        /// </summary>
        public List<GenericTypeParameter> GenericParameters { get; set; } = new List<GenericTypeParameter>();
    }

    /// <summary>
    /// Represents a property definition in an interface.
    /// </summary>
    public class PropertyDefinition
    {
        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the property.
        /// </summary>
        public required string Type { get; set; }

        /// <summary>
        /// Gets or sets whether the property has a getter.
        /// </summary>
        public bool HasGetter { get; set; } = true;

        /// <summary>
        /// Gets or sets whether the property has a setter.
        /// </summary>
        public bool HasSetter { get; set; } = true;

        /// <summary>
        /// Gets or sets the XML documentation for the property.
        /// </summary>
        public string? Documentation { get; set; }
    }

    /// <summary>
    /// Represents a parameter definition in a method.
    /// </summary>
    public class ParameterDefinition
    {
        /// <summary>
        /// Gets or sets the name of the parameter.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the parameter.
        /// </summary>
        public required string Type { get; set; }

        /// <summary>
        /// Gets or sets whether the parameter is optional.
        /// </summary>
        public bool IsOptional { get; set; }

        /// <summary>
        /// Gets or sets the default value for optional parameters.
        /// </summary>
        public string? DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets the XML documentation for the parameter.
        /// </summary>
        public string? Documentation { get; set; }
    }

    /// <summary>
    /// Represents a generic type parameter in an interface or method.
    /// </summary>
    public class GenericTypeParameter
    {
        /// <summary>
        /// Gets or sets the name of the generic type parameter.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the list of constraints on the generic type parameter.
        /// </summary>
        public List<string> Constraints { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the XML documentation for the generic type parameter.
        /// </summary>
        public string? Documentation { get; set; }
    }
}

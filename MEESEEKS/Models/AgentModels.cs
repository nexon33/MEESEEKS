using System;
using System.Collections.Generic;

namespace MEESEEKS.Models
{
    public class AgentConfiguration
    {
        public required string Id { get; set; }
        public required string ContainerImage { get; set; }
        public required Dictionary<string, string> EnvironmentVariables { get; set; }
        public required ResourceLimits ResourceLimits { get; set; }
        public required string WorkingDirectory { get; set; }
        public required string SolutionPath { get; set; }
    }

    public class ResourceLimits
    {
        public int CpuLimit { get; set; }
        public int MemoryLimitMB { get; set; }
        public int DiskSpaceLimitMB { get; set; }
    }

    public class AgentTask
    {
        public required string TaskId { get; set; }
        public TaskType Type { get; set; }
        public required Dictionary<string, object> Parameters { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }

    public class AgentResult
    {
        public required string TaskId { get; set; }
        public bool Success { get; set; }
        public required string Message { get; set; }
        public required Dictionary<string, object> Output { get; set; }
        public required List<string> Errors { get; set; }
    }

    public class CodeAnalysisResult
    {
        public required string FilePath { get; set; }
        public required List<CodeIssue> Issues { get; set; }
        public required List<CodeMetric> Metrics { get; set; }
        public required Dictionary<string, object> Context { get; set; }
    }

    public class CodeIssue
    {
        public required string Id { get; set; }
        public required string Description { get; set; }
        public IssueSeverity Severity { get; set; }
        public required CodeLocation Location { get; set; }
        public required string SuggestedFix { get; set; }
    }

    public class CodeLocation
    {
        public required string FilePath { get; set; }
        public int StartLine { get; set; }
        public int EndLine { get; set; }
        public int StartColumn { get; set; }
        public int EndColumn { get; set; }
    }

    public class CodeMetric
    {
        public required string Name { get; set; }
        public double Value { get; set; }
        public required string Unit { get; set; }
    }

    public class CodeModification
    {
        public required string FilePath { get; set; }
        public required string OriginalContent { get; set; }
        public required string ModifiedContent { get; set; }
        public required List<CodeChange> Changes { get; set; }
    }

    public class CodeChange
    {
        public ChangeType Type { get; set; }
        public required CodeLocation Location { get; set; }
        public required string OldText { get; set; }
        public required string NewText { get; set; }
    }

    public class GeneratedCode
    {
        public required string ClassName { get; set; }
        public required string Namespace { get; set; }
        public required string Code { get; set; }
        public required List<string> Dependencies { get; set; }
        public required List<string> Interfaces { get; set; }
    }

    public class UnitTestCode
    {
        public required string TestClassName { get; set; }
        public required string TestCode { get; set; }
        public required List<string> TestCases { get; set; }
    }

    public class InterfaceDefinition
    {
        public required string InterfaceName { get; set; }
        public required string Namespace { get; set; }
        public required List<MethodDefinition> Methods { get; set; }
        public required List<PropertyDefinition> Properties { get; set; }
    }

    public class MethodDefinition
    {
        public required string Name { get; set; }
        public required string ReturnType { get; set; }
        public required List<ParameterDefinition> Parameters { get; set; }
        public required List<string> Attributes { get; set; }
    }

    public class PropertyDefinition
    {
        public required string Name { get; set; }
        public required string Type { get; set; }
        public bool HasGetter { get; set; }
        public bool HasSetter { get; set; }
        public required List<string> Attributes { get; set; }
    }

    public class ParameterDefinition
    {
        public required string Name { get; set; }
        public required string Type { get; set; }
        public bool IsOptional { get; set; }
        public required string DefaultValue { get; set; }
    }

    public class AgentMessage
    {
        public required string SenderId { get; set; }
        public required string ReceiverId { get; set; }
        public required string MessageType { get; set; }
        public required Dictionary<string, object> Content { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class CodeGenerationRequest
    {
        public required string ClassName { get; set; }
        public required string Namespace { get; set; }
        public required List<string> Dependencies { get; set; }
        public required List<string> Interfaces { get; set; }
        public required List<MethodDefinition> Methods { get; set; }
    }

    public enum TaskType
    {
        CodeAnalysis,
        CodeGeneration,
        Refactoring,
        Testing,
        Documentation,
        DependencyUpdate
    }

    public enum TaskPriority
    {
        Low,
        Medium,
        High,
        Critical
    }

    public enum AgentStatus
    {
        Initializing,
        Ready,
        Busy,
        Error,
        ShuttingDown
    }

    public enum IssueSeverity
    {
        Info,
        Warning,
        Error,
        Critical
    }

    public enum ChangeType
    {
        Addition,
        Deletion,
        Modification
    }
}

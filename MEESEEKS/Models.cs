namespace Meeseeks.Core
{
    public class TaskDefinition
    {
        public string RepositoryUrl { get; set; }
        public string TaskDescription { get; set; }
        public TaskType Type { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
    }

    public class TaskContext
    {
        public bool RequiresSubtasks { get; set; }
        public string TargetFile { get; set; }
        public string TargetMethod { get; set; }
        public string CodeContext { get; set; }
        public IEnumerable<string> Dependencies { get; set; }
    }

    public class CodeChanges
    {
        public string FilePath { get; set; }
        public string OriginalContent { get; set; }
        public string ModifiedContent { get; set; }
        public string Description { get; set; }
    }

    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public string Error { get; set; }
    }

    public class AgentResult
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public CodeChanges Changes { get; set; }
    }

    public enum AgentStatus
    {
        Initialized,
        Working,
        Completed,
        Failed
    }

    public enum TaskType
    {
        Refactoring,
        CodeGeneration,
        Testing,
        Documentation,
        DependencyUpdate
    }
}

using System.Collections.Generic;

namespace MEESEEKS.Models.Task
{
    /// <summary>
    /// Represents the result of a task execution.
    /// </summary>
    public class AgentResult
    {
        /// <summary>
        /// Indicates whether the task was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Detailed message about the task execution.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Any errors that occurred during task execution.
        /// </summary>
        public List<string> Errors { get; set; } = new List<string>();

        /// <summary>
        /// Results produced by the task.
        /// </summary>
        public Dictionary<string, object> Results { get; set; } = new Dictionary<string, object>();
    }
}

using System;
using System.Collections.Generic;

namespace MEESEEKS.Models.Task
{
    /// <summary>
    /// Represents a task to be executed by a MEESEEKS agent.
    /// </summary>
    public class AgentTask
    {
        /// <summary>
        /// Unique identifier for the task.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Description of the task to be performed.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Type of the task (e.g., CodeGeneration, CodeAnalysis, Refactoring).
        /// </summary>
        public string TaskType { get; set; }

        /// <summary>
        /// Priority level of the task.
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// List of subtasks that this task has been divided into.
        /// </summary>
        public List<AgentTask> Subtasks { get; set; } = new List<AgentTask>();

        /// <summary>
        /// Parameters specific to this task.
        /// </summary>
        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
    }
}

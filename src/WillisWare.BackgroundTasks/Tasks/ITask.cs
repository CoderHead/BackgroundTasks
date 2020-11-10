using System;
using System.Threading;
using WillisWare.BackgroundTasks.Models;

namespace WillisWare.BackgroundTasks.Tasks
{
    /// <summary>
    /// Defines the required structure of a task.
    /// </summary>
    public interface ITask
    {
        /// <summary>
        /// Entry point for the service. Initializes the task status and triggers execution.
        /// </summary>
        /// <param name="taskRunId">A <see cref="Guid"/> value containing the unique run ID for this task.</param>
        /// <param name="description">A <see cref="string"/> value containing a description of this task, or this execution.</param>
        /// <param name="stoppingToken">A <see cref="CancellationToken"/> provided for short-circuiting task execution.</param>
        void Start(Guid taskRunId, string description = "", CancellationToken stoppingToken = default);

        /// <summary>
        /// Exit point for the service. Short-circuits task execution and exits with a <see cref="Models.TaskRunResult.Cancelled"/> result.
        /// </summary>
        void Stop();

        /// <summary>
        /// Gets a <see cref="string"/> value containing descriptive text about this task or the current execution.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets a <see cref="bool"/> value indicating whether the task is currently started/running.
        /// </summary>
        bool IsStarted { get; }

        /// <summary>
        /// Gets the type of <see cref="IRunnable"/> executed within this task.
        /// </summary>
        Type RunnableType { get; }

        /// <summary>
        /// Gets the <see cref="Models.TaskStatus"/> instance indicating the current/past state of the task.
        /// </summary>
        TaskStatus Status { get; }
    }
}

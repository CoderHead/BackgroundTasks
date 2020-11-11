using System;

namespace WillisWare.BackgroundTasks.Models
{
    /// <summary>
    /// Represents the current and past status of a runnable task.
    /// </summary>
    public sealed class TaskRunStatus
    {
        public Guid CurrentRunId { get; internal set; } = Guid.NewGuid();

        public DateTimeOffset CurrentStartTime { get; internal set; } = DateTimeOffset.Now;

        public TaskRunResult CurrentStatus { get; internal set; } = TaskRunResult.Unknown;

        public int FailCount { get; internal set; } = 0;

        public Exception LastException { get; internal set; }

        public string LastExceptionMessage { get; internal set; }

        public TaskRunResult LastResult { get; internal set; } = TaskRunResult.Unknown;

        public Guid? LastRunId { get; internal set; }

        public DateTimeOffset? LastRunTime { get; internal set; }

        public DateTimeOffset? LastSuccessTime { get; internal set; }

        public int SuccessCount { get; internal set; } = 0;
    }
}

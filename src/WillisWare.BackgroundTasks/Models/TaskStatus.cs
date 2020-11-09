using System;

namespace WillisWare.BackgroundTasks.Models
{
    public sealed class TaskStatus
    {
        public DateTimeOffset CurrentStartTime { get; set; } = DateTimeOffset.Now;

        public TaskRunResult CurrentStatus { get; set; } = TaskRunResult.Unknown;

        public string Description { get; set; }

        public int FailCount { get; set; } = 0;

        public Exception LastException { get; internal set; }

        public string LastExceptionMessage { get; internal set; }

        public TaskRunResult LastResult { get; internal set; } = TaskRunResult.Unknown;

        public Guid? LastRunId { get; internal set; }

        public DateTimeOffset? LastRunTime { get; internal set; }

        public DateTimeOffset? LastSuccessTime { get; internal set; }

        public int SuccessCount { get; set; } = 0;
    }
}

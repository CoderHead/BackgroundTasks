namespace WillisWare.BackgroundTasks.Models
{
    public enum TaskRunResult : byte
    {
        Unknown,
        Cancelled,
        Failure,
        Running,
        Success,
        Warning
    }
}

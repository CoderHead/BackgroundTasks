namespace WillisWare.BackgroundTasks.Tasks
{
    /// <summary>
    /// Defines the generic implementation of <see cref="ITask"/>.
    /// </summary>
    /// <typeparam name="TService">A reference type of runnable task with entry point.</typeparam>
    public interface ITask<TService> : ITask
    {
    }
}

using System.Threading;
using System.Threading.Tasks;
using WillisWare.BackgroundTasks.Tasks;

namespace WillisWare.BackgroundTasks
{
    /// <summary>
    /// Defines the required structure and entry point of a runnable task.
    /// </summary>
    public interface IRunnable
    {
        Task RunAsync(ITask task, CancellationToken cancellationToken);
    }
}

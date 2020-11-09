using System.Threading;
using System.Threading.Tasks;
using WillisWare.BackgroundTasks.Tasks;

namespace WillisWare.BackgroundTasks
{
    public interface IRunnable
    {
        Task RunAsync(ITask task, CancellationToken cancellationToken);
    }
}

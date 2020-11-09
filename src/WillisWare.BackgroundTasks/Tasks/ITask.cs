using System.Threading;
using WillisWare.BackgroundTasks.Models;

namespace WillisWare.BackgroundTasks.Tasks
{
    public interface ITask
    {
        void Start(CancellationToken cancellationToken);

        void Stop();

        bool IsStarted { get; }

        TaskStatus Status { get; }
    }
}

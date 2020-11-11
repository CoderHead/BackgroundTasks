using System.Threading;
using System.Threading.Tasks;
using WillisWare.BackgroundTasks.Tasks;

namespace WillisWare.BackgroundTasks.UnitTests.Fakes
{
    class FakeRunnable : IRunnable
    {
        public async Task RunAsync(ITask task, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }
}

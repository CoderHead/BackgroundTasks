using System;
using System.Threading;
using System.Threading.Tasks;
using WillisWare.BackgroundTasks.Tasks;

namespace WillisWare.BackgroundTasks.UnitTests.Fakes
{
    class FakeRunnableWithDelay : IRunnable
    {
        private readonly TimeSpan _delay;

        public FakeRunnableWithDelay(int delay)
        {
            _delay = TimeSpan.FromSeconds(delay);
        }

        public async Task RunAsync(ITask task, CancellationToken cancellationToken)
        {
            await Task.Delay(_delay);
        }
    }
}

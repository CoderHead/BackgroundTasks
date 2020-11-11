using System;
using System.Threading;
using System.Threading.Tasks;
using WillisWare.BackgroundTasks.Tasks;

namespace WillisWare.BackgroundTasks.UnitTests.Fakes
{
    class FakeRunnableWithException<TException> : IRunnable
        where TException : Exception
    {
        public async Task RunAsync(ITask task, CancellationToken cancellationToken)
        {
            await Task.FromException(Activator.CreateInstance<TException>());
        }
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WillisWare.BackgroundTasks.Tasks;

namespace WillisWare.BackgroundTasks
{
    public sealed class HostedService<TRunnable> : BackgroundService, ITask<TRunnable>
        where TRunnable : class, IRunnable
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;

        public HostedService(ILoggerFactory loggerFactory, IServiceProvider provider)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            _logger = loggerFactory
                .CreateLogger($"{GetType().Namespace}.{nameof(HostedService<TRunnable>)}<{typeof(TRunnable).FullName}>");

            _serviceProvider = provider;

            Status = new Models.TaskStatus();
        }

        public bool IsStarted => Status.CurrentStatus == Models.TaskRunResult.Running;

        public Models.TaskStatus Status { get; private set; }

        public void Start(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
        }
    }
}

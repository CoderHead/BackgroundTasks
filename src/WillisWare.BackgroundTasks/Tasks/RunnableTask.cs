using System;
using System.Threading;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WillisWare.BackgroundTasks.Tasks
{
    public sealed class RunnableTask<TRunnable> : BackgroundService, ITask
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;

        public RunnableTask(ILoggerFactory loggerFactory, IServiceProvider provider)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory), "This type cannot be instantiated without a logger.");
            }

            _logger = loggerFactory
                .CreateLogger($"{GetType().Namespace}.{nameof(RunnableTask<TRunnable>)}<{typeof(TRunnable).FullName}>");

            _serviceProvider = provider;
        }

        protected override System.Threading.Tasks.Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }

        public void Start(Guid taskRunId, string description = "", CancellationToken stoppingToken = default)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string Description { get; private set; }

        /// <inheritdoc />
        public bool IsStarted => Status.CurrentStatus == Models.TaskRunResult.Running;

        /// <inheritdoc />
        public Type RunnableType => typeof(TRunnable);

        /// <inheritdoc />
        public Models.TaskRunStatus Status { get; } = new Models.TaskRunStatus();
    }
}

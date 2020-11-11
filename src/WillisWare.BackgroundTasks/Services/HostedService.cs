using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WillisWare.BackgroundTasks.Exceptions;

namespace WillisWare.BackgroundTasks.Services
{
    /// <summary>
    /// Represents the concrete implementation of a hosted, runnable task. Allows for Start/Stop and manages the task status reporting.
    /// </summary>
    /// <typeparam name="TRunnable">A reference-type object that implements the <see cref="IRunnable"/> interface.</typeparam>
    public sealed class HostedService<TRunnable> : BackgroundService, IHostedService
        where TRunnable : class, IRunnable
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;

        public HostedService(ILoggerFactory loggerFactory, IServiceProvider provider)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory), "This type cannot be instantiated without a logger.");
            }

            _logger = loggerFactory
                .CreateLogger($"{GetType().Namespace}.{nameof(HostedService<TRunnable>)}<{typeof(TRunnable).FullName}>");

            _serviceProvider = provider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var startTime = DateTimeOffset.Now;

            await Task.Yield();
            try
            {
                Status.CurrentStatus = Models.TaskRunResult.Running;

                using (var scope = _serviceProvider.CreateScope())
                {
                    var runnable = scope.ServiceProvider.GetRequiredService<TRunnable>();
                    //await runnable.RunAsync(this, stoppingToken);
                }

                Status.FailCount = 0;
                Status.LastException = null;
                Status.LastExceptionMessage = string.Empty;
                Status.LastResult = Models.TaskRunResult.Success;
                Status.LastSuccessTime = DateTimeOffset.Now;
                Status.SuccessCount++;
            }
            catch (TaskRunWarning warn)
            {
                Status.LastException = warn;
                Status.LastExceptionMessage = warn.Message;
                Status.LastResult = Models.TaskRunResult.Warning;
            }
            catch (TaskRunException err)
            {
                Status.FailCount++;
                Status.LastException = err;
                Status.LastExceptionMessage = err.Message;
                Status.LastResult = Models.TaskRunResult.Failure;
                Status.SuccessCount = 0;
            }
            catch (Exception ex) // Treat default/unknown exception differently?
            {
                Status.FailCount++;
                Status.LastException = ex;
                Status.LastExceptionMessage = $"{ex.Message}\r\n{ex.StackTrace}";
                Status.LastResult = Models.TaskRunResult.Failure;
                Status.SuccessCount = 0;
            }
            finally
            {
                Status.CurrentStartTime = DateTimeOffset.MinValue;
                Status.CurrentStatus = Models.TaskRunResult.Unknown;
                Status.LastRunId = Status.CurrentRunId;
                Status.LastRunTime = startTime;
            }
        }

        /// <inheritdoc />
        public void Start(Guid taskRunId, string description = null, CancellationToken stoppingToken = default)
        {
            if (Status.CurrentStatus == Models.TaskRunResult.Running)
            {
                throw new InvalidOperationException("Task is already running.");
            }

            Status.CurrentRunId = taskRunId;
            Description = description ?? GetType().Name;

            StartAsync(stoppingToken).Wait();
        }

        /// <inheritdoc />
        public void Stop()
        {
            Status.CurrentStartTime = DateTimeOffset.MinValue;
            Status.CurrentStatus = Models.TaskRunResult.Unknown;
            Status.LastException = new TaskCanceledException();
            Status.LastResult = Models.TaskRunResult.Cancelled;
            Status.LastRunId = Status.CurrentRunId;
            Status.LastRunTime = DateTimeOffset.Now;

            StopAsync(CancellationToken.None).Wait();
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

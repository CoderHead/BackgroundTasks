using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WillisWare.BackgroundTasks.Tasks;

namespace WillisWare.BackgroundTasks.WebApiSample
{
    public class RunnableTask : IRunnable
    {
        private readonly ILogger<RunnableTask> _logger;

        public RunnableTask(ILogger<RunnableTask> logger)
        {
            _logger = logger;
        }

        public async Task RunAsync(ITask task, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Running task: {task.Description}");

            await Task.Delay(TimeSpan.FromSeconds(10));

            await Task.CompletedTask;

            _logger.LogInformation("Task completed");
        }
    }
}

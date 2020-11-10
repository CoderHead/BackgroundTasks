using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WillisWare.BackgroundTasks.Tasks;

namespace WillisWare.BackgroundTasks.WebApiSample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BackgroundTaskController : ControllerBase
    {
        private readonly ILogger<BackgroundTaskController> _logger;
        private readonly ITask<RunnableTask> _task;

        public BackgroundTaskController(ILogger<BackgroundTaskController> logger, ITask<RunnableTask> task)
        {
            _logger = logger;
            _task = task;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return await Task.FromResult(Ok(_task.Status));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Guid runId, string description)
        {
            if (_task.IsStarted)
            {
                return await Task.FromResult(Ok(_task.Status));
            }

            try
            {
                _logger.LogInformation($"Starting task {description} with run ID {runId}.");

                _task.Start(runId, description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception while executing task {description} with run ID {runId}.");

                return await Task.FromResult(StatusCode(500, _task.Status));
            }

            return await Task.FromResult(Ok(_task.Status));
        }
    }
}

using System;
using System.Net;
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
        private readonly ITask _task;

        public BackgroundTaskController(ILogger<BackgroundTaskController> logger, ITask task)
        {
            _logger = logger;
            _task = task;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync()
        {
            if (_task.IsStarted)
            {
                _task.Stop();
            }

            return await Task.FromResult(Ok(_task.Status));
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

                return await Task.FromResult(StatusCode((int)HttpStatusCode.InternalServerError, _task.Status));
            }

            return await Task.FromResult(Ok(_task.Status));
        }
    }
}

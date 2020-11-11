using System;
using System.Threading;
using WillisWare.BackgroundTasks.Models;

namespace WillisWare.BackgroundTasks.Services
{
    /// <summary>
    /// Defines the required structure of a hosted service.
    /// </summary>
    public interface IHostedService
    {
        /// <summary>
        /// Entry point for the service. Initializes the task status and triggers execution.
        /// </summary>
        /// <param name="serviceId">A <see cref="Guid"/> value containing the unique run ID for this service.</param>
        /// <param name="description">A <see cref="string"/> value containing a description of this service.</param>
        /// <param name="stoppingToken">A <see cref="CancellationToken"/> provided for short-circuiting service execution.</param>
        void Start(Guid serviceId, string description = "", CancellationToken stoppingToken = default);

        /// <summary>
        /// Exit point for the service. Short-circuits execution and exits with a <see cref="Models.TaskRunResult.Cancelled"/> result.
        /// </summary>
        void Stop();

        /// <summary>
        /// Gets a <see cref="string"/> value containing descriptive text about this service.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets a <see cref="bool"/> value indicating whether the service is currently started/running.
        /// </summary>
        bool IsStarted { get; }

        /// <summary>
        /// Gets the type of <see cref="IRunnable"/> executed within this service.
        /// </summary>
        Type RunnableType { get; }

        /// <summary>
        /// Gets the <see cref="Models.TaskRunStatus"/> instance indicating the current/past state of the service.
        /// </summary>
        TaskRunStatus Status { get; }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WillisWare.BackgroundTasks.Services;
using WillisWare.BackgroundTasks.Tasks;

namespace WillisWare.BackgroundTasks.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the necessary services into the <see cref="IServiceCollection"/> as a hosted service that starts immediately and runs in the background.
        /// </summary>
        /// <typeparam name="TRunnable">A reference-type object that implements the <see cref="IRunnable"/> interface.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> that accepts registrations.</param>
        /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddHostedTask<TRunnable>(this IServiceCollection services)
            where TRunnable : class, IRunnable
        {
            services.TryAddScoped<TRunnable>();
            services.AddHostedService<HostedService<TRunnable>>();

            return services;
        }

        /// <summary>
        /// Registers the necessary services into the <see cref="IServiceCollection"/> for manual execution in the background.
        /// </summary>
        /// <typeparam name="TRunnable">A reference-type object that implements the <see cref="IRunnable"/> interface.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> that accepts registrations.</param>
        /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddTask<TRunnable>(this IServiceCollection services)
            where TRunnable : class, IRunnable
        {
            services.TryAddScoped<TRunnable>();
            services.TryAddSingleton<ITask, RunnableTask<TRunnable>>();

            return services;
        }
    }
}

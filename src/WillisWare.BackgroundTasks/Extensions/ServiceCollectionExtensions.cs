using System.Linq;
using Microsoft.Extensions.DependencyInjection;
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
            var runnableType = typeof(TRunnable);
            if (!runnableType.IsAbstract && !services.Any(x => x.ServiceType == runnableType))
            {
                services.AddScoped<TRunnable>();
            }

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
            var runnableType = typeof(TRunnable);
            if (!runnableType.IsAbstract && !services.Any(x => x.ServiceType == runnableType))
            {
                services.AddScoped<TRunnable>();
            }

            services.AddSingleton<ITask<TRunnable>, HostedService<TRunnable>>();

            return services;
        }
    }
}

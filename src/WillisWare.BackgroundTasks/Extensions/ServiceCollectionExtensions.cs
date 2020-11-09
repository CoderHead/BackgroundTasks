using Microsoft.Extensions.DependencyInjection;

namespace WillisWare.BackgroundTasks.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTask<IRunnable>(this IServiceCollection services)
        {
            return services;
        }
    }
}

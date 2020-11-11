using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using WillisWare.BackgroundTasks.Extensions;
using WillisWare.BackgroundTasks.Tasks;
using WillisWare.BackgroundTasks.UnitTests.Fakes;

namespace WillisWare.BackgroundTasks.UnitTests
{
    public class ServiceCollectionTests
    {
        private IServiceCollection _services;

        [OneTimeSetUp]
        public void SetupOnce()
        {
            _services = new ServiceCollection();
        }

        [OneTimeTearDown]
        public void TearDownOnce()
        {
            _services = null;
        }

        [Test]
        public void AddTask_Registers_RunnableTask_Singleton()
        {
            _services.AddTask<FakeRunnable>();

            Assert.IsTrue(IsRegisteredServiceType<FakeRunnable>(ServiceLifetime.Scoped));
            Assert.IsTrue(IsRegisteredServiceType<ITask>(ServiceLifetime.Singleton));
            Assert.IsTrue(IsRegisteredImplementationType<RunnableTask<FakeRunnable>>(ServiceLifetime.Singleton));
        }

        private bool IsRegisteredImplementationType<T>(ServiceLifetime lifetime)
        {
            return _services.Any(x => x.ImplementationType == typeof(T) && x.Lifetime == lifetime);
        }

        private bool IsRegisteredServiceType<T>(ServiceLifetime lifetime)
        {
            return _services.Any(x => x.ServiceType == typeof(T) && x.Lifetime == lifetime);
        }
    }
}

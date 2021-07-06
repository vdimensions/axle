using System.Collections.Generic;
using System.Linq;
using Axle.DependencyInjection;
using NUnit.Framework;

namespace Axle.Data.Npgsql.Tests
{
    public class ModularityTests
    {
        [Test]
        public void TestNpgsqlProviderIsRegistered()
        {
            IDependencyContainer container = null;
            using (Axle.Application.Application.Build().ConfigureDependencies(c => container = c).UsePostgreSql().Run())
            {
                var providers = container.Resolve<IEnumerable<IDbServiceProvider>>().ToArray();
                Assert.IsNotEmpty(providers, "No database service providers have been registered");
                Assert.True(providers.Length == 1, "Only one database service provider is expected.");
                Assert.AreEqual(providers[0].ProviderName, NpgsqlServiceProvider.Name);
            }
        }
    }
}
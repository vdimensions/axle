using System.Collections.Generic;
using System.Linq;
using Axle;
using Axle.Configuration;
using Axle.Data.Configuration;
using Axle.DependencyInjection;
using NUnit.Framework;

namespace Axle.Data.MySql.Tests
{
    public class ModularityTests
    {
        [Test]
        public void TestMySqlProviderIsRegistered()
        {
            IContainer container = null;
            var appBuilder = Application.Build()
                .ConfigureDependencies(c => container = c)
                .LoadMySqlModule();
            using (appBuilder.Run())
            {
                var providers = container.Resolve<IEnumerable<IDbServiceProvider>>().ToArray();
                Assert.IsNotEmpty(providers, "No database service providers have been registered");
                Assert.True(providers.Length == 1, "Only one database service provider is expected.");
                Assert.AreEqual(providers[0].ProviderName, MySqlServiceProvider.Name);
            }
        }
        [Test]
        public void TestConnectionStringBinding()
        {
            IContainer container = null;
            var appBuilder = Application.Build()
                .ConfigureDependencies(c => container = c)
                .AddLegacyConfig()
                .LoadMySqlModule();
            using (appBuilder.Run())
            {
                var cfg = container.Resolve<IConfiguration>();
                var connectionStrings = cfg.GetSection("connectionStrings");
                var defaultConnectionString = connectionStrings.GetSection<ConnectionStringInfo>("default");

                Assert.IsNotNull(defaultConnectionString);
            }
        }
        [Test]
        public void TestConnectionStringsBinding()
        {
            IContainer container = null;
            var appBuilder = Application.Build()
                .ConfigureDependencies(c => container = c)
                .AddLegacyConfig()
                .LoadMySqlModule();
            using (appBuilder.Run())
            {
                var cfg = container.Resolve<IConfiguration>();
                var defaultConnectionString = cfg.GetSection<Dictionary<string, ConnectionStringInfo>>("connectionStrings");

                Assert.IsNotNull(defaultConnectionString);
            }
        }
    }
}
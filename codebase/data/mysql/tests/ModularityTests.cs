using System.Collections.Generic;
using System.Linq;
using Axle;
using Axle.Configuration;
using Axle.Data.Configuration;
using Axle.Data.DataSources;
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
                .UseMySql();
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
                .UseMySql();
            using (appBuilder.Run())
            {
                var cfg = container.Resolve<IConfiguration>();
                var connectionStrings = cfg.GetConnectionStrings();
                var defaultConnectionString = connectionStrings.Where(x => x.Name.Equals("default"));

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
                .UseMySql();
            using (appBuilder.Run())
            {
                var cfg = container.Resolve<IConfiguration>();
                var connectionStrings = cfg.GetConnectionStrings();

                Assert.IsNotNull(connectionStrings);
                Assert.IsNotEmpty(connectionStrings);
            }
        }

        [Test]
        public void TestDataSourceDiscovery()
        {
            IContainer container = null;
            var appBuilder = Application.Build()
                .ConfigureDependencies(c => container = c)
                .AddLegacyConfig()
                .UseDataSources()
                .UseMySql();
            using (appBuilder.Run())
            {
                var cfg = container.Resolve<IConfiguration>();
                var connectionStrings = cfg.GetConnectionStrings();

                Assert.IsNotNull(connectionStrings);

                foreach (var connectionStringInfo in connectionStrings)
                {
                    var resolved = container.TryResolve<DataSource>(out var dataSource, connectionStringInfo.Name);
                    Assert.IsTrue(resolved);
                    Assert.IsNotNull(dataSource);
                    Assert.AreEqual(dataSource.Name, connectionStringInfo.Name);
                }
            }
        }
    }
}
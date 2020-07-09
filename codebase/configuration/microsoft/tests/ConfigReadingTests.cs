using System.IO;
using System.Linq;
using System.Reflection;
using Axle.Configuration.Legacy;
using Axle.Configuration.Microsoft;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using NUnit.Framework;

namespace Axle.Configuration.Tests
{
    public class ConfigReadingTests
    {
        public class SubjectSection
        {
            public string Name { get; set; }
        }
        public class GreetingsSection
        {
            public string Default { get; set; }
        }
        
        [Test]
        public void Test()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("jsconfig.json");
            var cfg = builder.Build();

            var greetings = cfg.GetSection("greetings");
            Assert.IsNotNull(greetings);

            var defaultGreeting = greetings["default"];
            Assert.IsNotNull(defaultGreeting);
        }

        [Test]
        public void TestStreamedJsonConfig()
        {
            var cfgPath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\jsconfig.json";
            var cfg = new LayeredConfigManager()
                .Append(new StreamedFileConfigSource<JsonConfigurationSource>(File.OpenRead(cfgPath)))
                .LoadConfiguration();

            var greetings = cfg.GetSection("greetings");
            Assert.IsNotNull(greetings);

            var defaultGreeting = greetings["default"];
            Assert.IsNotNull(defaultGreeting);
        }

        [Test]
        public void TestLayeredConfig()
        {
            var cfgPath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\jsconfig.json";
            var cfg = new LayeredConfigManager()
                .Append(new StreamedFileConfigSource<JsonConfigurationSource>(File.OpenRead(cfgPath)))
                .Append(new LegacyConfigSource())
                .LoadConfiguration();

            // present in appSettings
            var messageFormat = cfg["messageFormat"];
            Assert.IsNotNull(messageFormat);

            // present in jsconfig.json
            var subject = cfg["subject"];
            Assert.IsNotNull(subject);

            var greetings = cfg.GetSection("greetings");
            Assert.IsNotNull(greetings);

            var defaultGreeting = greetings["default"];
            Assert.IsNotNull(defaultGreeting);
        }

        [Test]
        public void TestSectionBinding()
        {
            var cfgPath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\jsconfig.json";
            var cfg = new LayeredConfigManager()
                .Append(new StreamedFileConfigSource<JsonConfigurationSource>(File.OpenRead(cfgPath)))
                .Append(new LegacyConfigSource())
                .LoadConfiguration();

            var subject = cfg.GetSection<SubjectSection>("subject");
            var greeting = cfg.GetSection<GreetingsSection>("greetings");
            var messageFormat = cfg["messageFormat"].Select(x => x.Value).SingleOrDefault();
            Assert.IsNotNull(messageFormat);
            Assert.AreEqual("Hello, World", string.Format(messageFormat, greeting.Default, subject.Name));
        }

        [Test]
        public void TestMSConfig()
        {
            var cfg = new LegacyConfigSource().LoadConfiguration();
            Assert.IsNotNull(cfg);

            var message = cfg["messageFormat"];
            Assert.IsNotNull(message);
        }
    }
}
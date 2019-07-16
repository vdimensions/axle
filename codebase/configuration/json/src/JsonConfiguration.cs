using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Axle.Configuration.Json
{
    public sealed class JsonConfigurationReader : Axle.Configuration.IConfigurationReader
    {
        public IConfiguration LoadEmbeddedConfiguration(Assembly assembly, string resourceName)
        {
            throw new NotImplementedException();
        }

        public IConfiguration LoadEmbeddedConfiguration(Assembly assembly)
        {
            throw new NotImplementedException();
        }

        public IConfiguration CurrentConfiguration { get; }
    }

    public class JsonConfiguration : IConfiguration
    {
        public IConfigurationSection GetSection(Type type)
        {
            throw new NotImplementedException();
        }

        public T GetSection<T>() where T : class, IConfigurationSection
        {
            throw new NotImplementedException();
        }

        public T GetSection<T>(bool useDefault) where T : class, IConfigurationSection, new()
        {
            throw new NotImplementedException();
        }

        public string Path { get; }
    }
}

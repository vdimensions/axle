using System;


namespace Axle.Configuration
{
    /// <summary>
    /// An interface reperesenting the system configuration.
    /// </summary>
    public interface IConfiguration
    {
        string Path { get; }
        IConfigurationSection GetSection(Type type);
        T GetSection<T>() where T: class, IConfigurationSection;
        T GetSection<T>(bool useDefault) where T: class, IConfigurationSection, new();
    }
}
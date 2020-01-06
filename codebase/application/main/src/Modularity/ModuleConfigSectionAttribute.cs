using System;

namespace Axle.Modularity
{
    public sealed class ModuleConfigSectionAttribute : Attribute
    {
        public ModuleConfigSectionAttribute(Type type, string name)
        {
            Type = type;
            Name = name;
        }

        /// <summary>
        /// Gets the type of the configuration section object that addresses the target module.
        /// </summary>
        public Type Type { get; }
        
        /// <summary>
        /// Gets the name of the configuration section representing the target module's configuration.
        /// </summary>
        public string Name { get; }
    }
}
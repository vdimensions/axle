using System;

namespace Axle.Modularity
{
    /// <summary>
    /// An attribute that is used to specify configuration objects associated with a module.
    /// If a module-specific configuration is found, specified section (or the entire configuration if a section name
    /// is omitted) will be loaded and converted to a specified configuration object type.
    /// The so produced configuration object instance will be made available for dependency injection in the target
    /// module by the axle module loader. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class ModuleConfigSectionAttribute : Attribute
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ModuleConfigSectionAttribute"/> using the specified
        /// configuration <paramref name="type"/> and its corresponding config section <paramref name="name"/>.
        /// </summary>
        /// <param name="type">
        /// The type of the configuration object represented by the referenced config section.
        /// </param>
        /// <param name="name">
        /// The name of the config section.
        /// </param>
        public ModuleConfigSectionAttribute(Type type, string name)
        {
            Type = type;
            Name = name;
        }
        /// <summary>
        /// Creates a new instance of the <see cref="ModuleConfigSectionAttribute"/> using the specified
        /// configuration <paramref name="type"/>.
        /// </summary>
        /// <param name="type">
        /// The type of the configuration object represented by the referenced configuration.
        /// </param>
        public ModuleConfigSectionAttribute(Type type) : this(type, string.Empty) { }

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
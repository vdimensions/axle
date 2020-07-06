using System;

namespace Axle.Modularity
{
    internal interface IModuleReferenceAttribute
    {
        /// <summary>
        /// Gets the type of the module that is being referenced by the target module.
        /// </summary>
        Type ModuleType { get; }
        /// <summary>
        /// Gets a <see cref="bool">boolean</see> value indicating if the dependency established by the current
        /// <see cref="IModuleReferenceAttribute"/> implementation is mandatory.
        /// <para>
        /// For example, if the value is <c>true</c>, then the <see cref="ModuleType"/> module will be set to initialize
        /// before the target module initializes. In case that module is not possible to be initialized for some reason,
        /// an exception will be thrown. The presumption is that the target module cannot function normally without its
        /// mandatory dependencies.
        /// </para>
        /// <para>
        /// In case this value is set to <c>false</c>, then the <see cref="ModuleType"/> module will be set to
        /// initialize before the target module only if it was selected for initialization (see
        /// <see cref="Application.Builder"/> for details on enlisting modules for loading). It is assumed the target
        /// module will be able to operate without non-mandatory dependencies, so in case any of those modules are not
        /// found, then no exception will be thrown.
        /// </para>
        /// </summary>
        bool Mandatory { get; }
    }
}
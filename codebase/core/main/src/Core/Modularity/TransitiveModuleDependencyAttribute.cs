using System;
using System.ComponentModel;
using System.Diagnostics;

using Axle.Verification;


namespace Axle.Core.Modularity
{
    /// <summary>
    /// An attribute that enables an interface to cause a transitive dependency on a given module.
    /// </summary>
    /// <remarks>
    /// The attribute takes effect only when applied to interfaces implemented by an <see cref="IModuleDependency">application module</see>.
    /// </remarks>
    /// <seealso cref="ModuleDependencyAttribute"/>
    /// <seealso cref="IApplicationModule"/>
    [AttributeUsage(AttributeTargets.Interface, Inherited = true, AllowMultiple = true)]
    public sealed class TransitiveModuleDependencyAttribute : Attribute, IModuleDependency
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string dependencyName;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool notifyOnInit = true;

        /// <summary>
        /// Creates a new instance of <see cref="TransitiveModuleDependencyAttribute"/> with the specified dependency module.
        /// </summary>
        /// <param name="dependencyName">
        /// The name of the dependency module.
        /// </param>
        public TransitiveModuleDependencyAttribute(string dependencyName)
        {
            this.dependencyName = dependencyName.VerifyArgument("dependencyModule").IsNotNull();
        }

        public string DependencyName { get { return dependencyName; } }

        [DefaultValue(true)]
        public bool NotifyOnInit
        {
            get { return  notifyOnInit; }
            set { notifyOnInit = value; }
        }

        bool IModuleDependency.ShareDependencies { get { return false; } }
    }
}
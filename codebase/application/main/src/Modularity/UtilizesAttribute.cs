using System;
using System.Diagnostics.CodeAnalysis;
#if NETSTANDARD || NET45_OR_NEWER
using System.Reflection;
#endif

using Axle.Verification;


namespace Axle.Modularity
{
    /// <summary>
    /// An attribute that is used to establish a specified module as an optional dependency on the target module.
    /// </summary>
    /// <seealso cref="RequiresAttribute"/>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = true)]
    public class UtilizesAttribute : Attribute, IModuleReferenceAttribute
    {
        #if NETSTANDARD || NET45_OR_NEWER
        internal static string TypeToString(Type type) => $"{type.FullName}, {type.GetTypeInfo().Assembly.GetName().Name}";
        #else
        internal static string TypeToString(Type type) => $"{type.FullName}, {type.Assembly.GetName().Name}";
        #endif

        /// <summary>
        /// Initializes a new instance of the <see cref="UtilizesAttribute"/> class.
        /// </summary>
        /// <param name="moduleType">
        /// The type of the module that will become a dependency for the target module.
        /// </param>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public UtilizesAttribute(Type moduleType)
        {
            moduleType.VerifyArgument(nameof(moduleType)).IsNotNull();
            ModuleType = moduleType;
        }

        /// <inheritdoc />
        public Type ModuleType { get; }

        bool IModuleReferenceAttribute.Mandatory => false;
    }
}
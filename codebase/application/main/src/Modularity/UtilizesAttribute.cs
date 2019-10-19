using System;
using System.Diagnostics.CodeAnalysis;
#if NETSTANDARD || NET45_OR_NEWER
using System.Reflection;
#endif

using Axle.Verification;


namespace Axle.Modularity
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = true)]
    public class UtilizesAttribute : Attribute, IUsesAttribute
    {
        #if NETSTANDARD || NET45_OR_NEWER
        internal static string TypeToString(Type type) => $"{type.FullName}, {type.GetTypeInfo().Assembly.GetName().Name}";
        #else
        internal static string TypeToString(Type type) => $"{type.FullName}, {type.Assembly.GetName().Name}";
        #endif

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public UtilizesAttribute(string module)
        {
            Module = module.VerifyArgument(nameof(module)).IsNotNullOrEmpty();
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public UtilizesAttribute(Type moduleType)
        {
            Module = TypeToString(moduleType.VerifyArgument(nameof(moduleType)).IsNotNull());
        }

        public string Module { get; }

        Type IUsesAttribute.ModuleType => Type.GetType(Module);

        bool IUsesAttribute.Required => false;
    }
}
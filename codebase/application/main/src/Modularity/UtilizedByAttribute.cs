using System;
using System.Diagnostics.CodeAnalysis;

using Axle.Verification;


namespace Axle.Modularity
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = true)]
    public class UtilizedByAttribute : Attribute
    {
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public UtilizedByAttribute(string module)
        {
            Module = module.VerifyArgument(nameof(module)).IsNotNullOrEmpty();
        }
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public UtilizedByAttribute(Type moduleType)
        {
            Module = UtilizesAttribute.TypeToString(moduleType.VerifyArgument(nameof(moduleType)).IsNotNull());
        }

        internal bool Accepts(Type type) => UtilizesAttribute.TypeToString(type.VerifyArgument(nameof(type))).Equals(Module, StringComparison.Ordinal);

        public string Module { get; }
    }
}
#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Configuration.Legacy.Sdk;
using Axle.Verification;

namespace Axle.Configuration.Legacy
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class TypeSafetyValidator<T> : AbstractConfigValidator<Type>
    {
        protected override void Validate(Type value)
        {
            value.VerifyArgument(nameof(value)).Is<T>();
        }
    }
}
#endif
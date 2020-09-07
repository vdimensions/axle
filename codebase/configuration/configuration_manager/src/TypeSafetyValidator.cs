#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Configuration.ConfigurationManager.Sdk;
using Axle.Verification;

namespace Axle.Configuration.ConfigurationManager
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class TypeSafetyValidator<T> : AbstractConfigValidator<Type>
    {
        protected override void Validate(Type value)
        {
            TypeVerifier.Is<T>(Verifier.VerifyArgument(value, nameof(value)));
        }
    }
}
#endif
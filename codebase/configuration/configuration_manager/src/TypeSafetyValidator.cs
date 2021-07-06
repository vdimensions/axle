#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Configuration.ConfigurationManager.Sdk;
using Axle.Verification;

namespace Axle.Configuration.ConfigurationManager
{
    /// <summary>
    /// A <see cref="AbstractConfigValidator{T}"/> implementation that works on values representing a
    /// <see cref="Type"/>. The validator determines a type as valid if it is the type represented by the
    /// generic type parameter <typeparamref name="T"/>, or a type in the inheritance hierarchy of
    /// <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The <see cref="Type"/> that the current <see cref="TypeSafetyValidator{T}"/> instance will base its validation
    /// on. Only values representing either the <typeparamref name="T"/> or a type in its inheritance hierarchy will be
    /// accepted.
    /// </typeparam>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class TypeSafetyValidator<T> : AbstractConfigValidator<Type>
    {
        /// <inheritdoc />
        protected override void Validate(Type value)
        {
            TypeVerifier.Is<T>(Verifier.VerifyArgument(value, nameof(value)));
        }
    }
}
#endif
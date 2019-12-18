using Axle.Verification;
using System;

namespace Axle.Reflection
{
    public sealed class PartiallyGenericMethodToken : MethodToken, IGenericMethod, IPartiallyGenericMethod
    {
        public PartiallyGenericMethodToken(IMethod rawMethod, Type[] genericArguments) : base(rawMethod.ReflectedMember)
        {
            RawMethod = Verifier.IsNotNull(Verifier.VerifyArgument(rawMethod, nameof(rawMethod))).Value;
            GenericArguments = Verifier.IsNotNull(Verifier.VerifyArgument(genericArguments, nameof(genericArguments))).Value;
        }

        public Type[] GenericArguments { get; }

        public IMethod RawMethod { get; }
    }
}
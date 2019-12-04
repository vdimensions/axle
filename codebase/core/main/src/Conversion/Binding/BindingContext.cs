using Axle.Verification;

namespace Axle.Conversion.Binding
{
    public sealed class BindingContext
    {
        public BindingContext(IBindingMemberInfoProvider memberInfoProvider, IBindingConverter converter)
        {
            MemberInfoProvider = Verifier.IsNotNull(Verifier.VerifyArgument(memberInfoProvider, nameof(memberInfoProvider))).Value;
            Converter = Verifier.IsNotNull(Verifier.VerifyArgument(converter, nameof(converter))).Value;
        }

        public IBindingMemberInfoProvider MemberInfoProvider { get; }
        public IBindingMemberValueProvider MemberValueProvider { get; }
        public IBindingConverter Converter { get; }
    }
}

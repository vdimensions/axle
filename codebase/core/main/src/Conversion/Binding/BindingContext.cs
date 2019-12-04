using Axle.Verification;

namespace Axle.Conversion.Binding
{
    public sealed class BindingContext
    {
        public BindingContext(IBindingObjectInfoProvider objectInfoProvider, IBindingConverter converter)
        {
            ObjectInfoProvider = Verifier.IsNotNull(Verifier.VerifyArgument(objectInfoProvider, nameof(objectInfoProvider))).Value;
            Converter = Verifier.IsNotNull(Verifier.VerifyArgument(converter, nameof(converter))).Value;
        }

        public IBindingObjectInfoProvider ObjectInfoProvider { get; }
        public IBindingMemberValueProvider MemberValueProvider { get; }
        public IBindingConverter Converter { get; }
    }
}

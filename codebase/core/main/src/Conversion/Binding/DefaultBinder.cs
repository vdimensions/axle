using Axle.Verification;
using System;

namespace Axle.Conversion.Binding
{
    public class DefaultBinder : IBinder
    {
        private bool TryBind(BindingContext ctx, IBindingMemberValueProvider valueProvider, object instance, Type targetType, out object boundValue)
        {
            if (valueProvider is ISimpleMemberValueProvider svp)
            {
                return ctx.Converter.TryConvertMemberValue(svp.Value, targetType, out boundValue);
            }
            else if (valueProvider is IComplexMemberValueProvider cvp)
            {
                var members = ctx.MemberInfoProvider.GetMembers(instance);
                foreach (var member in members)
                {
                    if (cvp.TryGetValue(member.Name, out var memberValueProvider) 
                        && ctx.MemberInfoProvider.TryCreateInstance(member.GetType(), out var memberInstance)
                        && TryBind(ctx, memberValueProvider, memberInstance, member.GetType(), out var memberValue))
                    {
                        member.SetAccessor.SetValue(instance, memberValue);
                    }
                }
                boundValue = instance;
                return true;
            }
            boundValue = null;
            return false;
        }

        /// <inheritdoc>
        public object Bind(BindingContext bindingContext, object instance)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(bindingContext, nameof(bindingContext)));
            Verifier.IsNotNull(Verifier.VerifyArgument(instance, nameof(instance)));
            return TryBind(bindingContext, bindingContext.MemberValueProvider, instance, instance.GetType(), out var boundValue) 
                ? boundValue 
                : instance;
        }

        /// <inheritdoc>
        public object Bind(BindingContext bindingContext, Type type)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(bindingContext, nameof(bindingContext)));
            Verifier.IsNotNull(Verifier.VerifyArgument(type, nameof(type)));
            return bindingContext.MemberInfoProvider.TryCreateInstance(type, out var instance) 
                && TryBind(bindingContext, bindingContext.MemberValueProvider, instance, type, out var boundValue) 
                    ? boundValue 
                    : instance;
        }
    }
}

#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using Axle.Verification;

namespace Axle.Configuration.Legacy.Sdk
{
    public abstract class AbstractConfigValidator : System.Configuration.ConfigurationValidatorBase
    {
        
    }
    public abstract class AbstractConfigValidator<T> : AbstractConfigValidator
    {
        public sealed override void Validate(object value)
        {
            switch (value)
            {
                case T variable:
                    Validate(variable);
                    break;
                case null:
                    return;
                default:
                    throw new ArgumentTypeMismatchException<T>(nameof(value), value.GetType());
            }
        }
        protected abstract void Validate(T value);
    }
}
#endif
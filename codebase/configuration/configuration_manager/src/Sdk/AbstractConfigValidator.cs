#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using Axle.Verification;

namespace Axle.Configuration.ConfigurationManager.Sdk
{
    /// <summary>
    /// An abstract class to serve as a base for implementing a configuration validator.
    /// </summary>
    /// <seealso cref="System.Configuration.ConfigurationValidatorBase"/>
    public abstract class AbstractConfigValidator : System.Configuration.ConfigurationValidatorBase { }
    /// <summary>
    /// A generic base class to be used gor custom implementations of the <see cref="AbstractConfigValidator"/> class.
    /// </summary>
    /// <typeparam name="T">
    /// The type of configuration value to verify.
    /// </typeparam>
    public abstract class AbstractConfigValidator<T> : AbstractConfigValidator
    {
        /// <inheritdoc />
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
        /// <summary>
        /// Determines whether the <paramref name="value" /> of an object is valid.
        /// </summary>
        /// <param name="value">
        /// The value of <typeparamref name="T"/> to be validated.
        /// </param>
        protected abstract void Validate(T value);
    }
}
#endif
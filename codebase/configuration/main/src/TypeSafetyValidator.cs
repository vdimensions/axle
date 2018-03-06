using System;

using Axle.Configuration.Sdk;
using Axle.Verification;


namespace Axle.Configuration
{
    public class TypeSafetyValidator<T> : AbstractConfigValidator<Type>
    {
        protected override void Validate(Type value)
        {
            value.VerifyArgument(nameof(value)).Is<T>();
        }
    }
}

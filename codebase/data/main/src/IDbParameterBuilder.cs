using System;
using System.Data;
using Axle.Builder;
using Axle.Verification;


namespace Axle.Data
{
    public interface IDbParameterBuilder
    {
        IDbParameterValueBuilder CreateParameter(string name, ParameterDirection direction);
    }

    /// <summary>
    /// A static class to provide common extension methods to implementations of the <see cref="IDbParameterBuilder"/> interface.
    /// </summary>
    public static class DbParameterBuilderExtensions
    {
        public static IDataParameter CreateInputParameter(this IDbParameterBuilder builder, string name, Func<IDbParameterValueBuilder, IDbParameterOptionalPropertiesBuilder> buildFunc)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(builder, nameof(builder)));
            Verifier.IsNotNull(Verifier.VerifyArgument(buildFunc, nameof(buildFunc)));
            var p = buildFunc(builder.CreateParameter(name, ParameterDirection.Input));
            return ((IFluentBuilder<IDataParameter>) p).Build();
        }

        public static IDataParameter CreateInputOutputParameter(this IDbParameterBuilder builder, string name, Func<IDbParameterValueBuilder, IDbParameterOptionalPropertiesBuilder> buildFunc)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(builder, nameof(builder)));
            Verifier.IsNotNull(Verifier.VerifyArgument(buildFunc, nameof(buildFunc)));
            var p = buildFunc(builder.CreateParameter(name, ParameterDirection.InputOutput));
            return ((IFluentBuilder<IDataParameter>) p).Build();
        }

        public static IDataParameter CreateOutputParameter(this IDbParameterBuilder builder, string name, Func<IDbParameterTypeBuilder, IDbParameterOptionalPropertiesBuilder> buildFunc)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(builder, nameof(builder)));
            Verifier.IsNotNull(Verifier.VerifyArgument(buildFunc, nameof(buildFunc)));
            var p = buildFunc(builder.CreateParameter(name, ParameterDirection.Output));
            return ((IFluentBuilder<IDataParameter>) p).Build();
        }

        public static IDataParameter CreateReturnParameter(this IDbParameterBuilder builder, string name, Func<IDbParameterTypeBuilder, IDbParameterOptionalPropertiesBuilder> buildFunc)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(builder, nameof(builder)));
            Verifier.IsNotNull(Verifier.VerifyArgument(buildFunc, nameof(buildFunc)));
            var p = buildFunc(builder.CreateParameter(name, ParameterDirection.ReturnValue));
            return ((IFluentBuilder<IDataParameter>) p).Build();
        }
    }
}
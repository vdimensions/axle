namespace Axle.Data.DataSources
{
    public interface ICommandBuilder
    {
        //ICommandBuilder AddParameter(IDataParameter parameter);
        //ICommandBuilder AddParameter(Func<IDbParameterBuilder, IDbParameterOptionalPropertiesBuilder> buildFunc);
        //
        //ICommandBuilder AddInputParameter(string name, Func<IDbParameterValueBuilder, IDbParameterOptionalPropertiesBuilder> buildFunc);
        //
        //ICommandBuilder AddInputOutputParameter(string name, Func<IDbParameterValueBuilder, IDbParameterOptionalPropertiesBuilder> buildFunc);
        //
        //ICommandBuilder AddOutputParameter(string name, Func<IDbParameterTypeBuilder, IDbParameterOptionalPropertiesBuilder> buildFunc);
        //
        //ICommandBuilder AddReturnParameter(string name, Func<IDbParameterTypeBuilder, IDbParameterOptionalPropertiesBuilder> buildFunc);

        ICommandBuilder SetTimeout(int timeout);
    }
}
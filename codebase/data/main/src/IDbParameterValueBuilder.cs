namespace Axle.Data
{
    public interface IDbParameterValueBuilder
    {
        IDbParameterOptionalPropertiesBuilder SetValue(object value);
    }
}
using System.Data;


namespace Axle.Data
{
    public interface IDbParameterBuilder
    {
        IDbParameterValueBuilder CreateParameter(string name, ParameterDirection direction);
    }
}
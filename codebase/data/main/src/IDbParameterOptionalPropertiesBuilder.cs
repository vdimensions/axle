using System.Data;

namespace Axle.Data
{
    public interface IDbParameterOptionalPropertiesBuilder
    {
        IDbParameterOptionalPropertiesBuilder SetType(DbType type);
        IDbParameterOptionalPropertiesBuilder SetSize(int size);
        IDataParameter Build();
    }
}
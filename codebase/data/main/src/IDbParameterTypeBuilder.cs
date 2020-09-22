using System.Data;

namespace Axle.Data
{
    public interface IDbParameterTypeBuilder
    {
        IDbParameterOptionalPropertiesBuilder SetType(DbType type);
    }
}
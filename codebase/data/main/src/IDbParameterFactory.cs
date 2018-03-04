using System.Data;
using System.Data.Common;


namespace Axle.Data
{
    public interface IDbParameterFactory<TDbParameter, TDbType>
        where TDbParameter: DbParameter
        where TDbType: struct
    {
        TDbParameter CreateDbParameter(string name, TDbType? type, int? size, ParameterDirection direction, object value);
    }
}
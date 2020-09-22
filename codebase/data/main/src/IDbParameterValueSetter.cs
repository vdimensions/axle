using System.Data;

namespace Axle.Data
{
    public interface IDbParameterValueSetter
    {
        void SetValue(IDataParameter parameter, DbType type, object value);
    }
    public interface IDbParameterValueSetter<TDbParameter, TDbType> : IDbParameterValueSetter where TDbParameter: IDataParameter where TDbType: struct
    {
        void SetValue(TDbParameter parameter, TDbType type, object value);
    }
}

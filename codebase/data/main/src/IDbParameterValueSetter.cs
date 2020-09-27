using System.Data;

namespace Axle.Data
{
    /// <summary>
    /// An interface that is used to set values to <see cref="IDbDataParameter"/> instances.
    /// <para>
    /// Often, a <see cref="IDbServiceProvider"/> will internally employ its own implementation of this interface
    /// to correctly convert managed type values into their respective counterparts which the
    /// database represented by the <see cref="IDbServiceProvider"/> understands and assign them to the
    /// <see cref="IDbDataParameter"/>'s <see cref="IDataParameter.Value"/>'s property. 
    /// </para>
    /// </summary>
    /// <see cref="IDbDataParameter"/>
    /// <see cref="DbType"/>
    public interface IDbParameterValueSetter
    {
        void SetValue(IDataParameter parameter, DbType type, object value);
    }
    public interface IDbParameterValueSetter<TDbParameter, TDbType> : IDbParameterValueSetter where TDbParameter: IDataParameter where TDbType: struct
    {
        void SetValue(TDbParameter parameter, TDbType type, object value);
    }
}

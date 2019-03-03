using System.Collections.Generic;
using System.Data;

using Axle.Data.Conversion;
using Axle.Verification;


namespace Axle.Data.Common
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public abstract class DbParameterValueSetter<TDbParameter, TDbType> : IDbParameterValueSetter<TDbParameter, TDbType> 
        where TDbParameter: IDataParameter
        where TDbType: struct
    {
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        [System.Serializable]
        #endif
        protected sealed class DbTypeEqualityComparer : IEqualityComparer<DbType>
        {
            public bool Equals(DbType x, DbType y) { return x == y; }

            public int GetHashCode(DbType obj) { return (int)obj; }
        }

        private readonly IDictionary<DbType, IDbValueConverter> _dbTypeConverters;
        private readonly IDictionary<TDbType, IDbValueConverter> _tdbTypeConverters;

        protected DbParameterValueSetter() : this(EqualityComparer<TDbType>.Default) { }
        protected DbParameterValueSetter(IEqualityComparer<TDbType> comparer)
        {
            _dbTypeConverters = new Dictionary<DbType, IDbValueConverter>(new DbTypeEqualityComparer());
            _tdbTypeConverters = new Dictionary<TDbType, IDbValueConverter>(comparer.VerifyArgument(nameof(comparer)).IsNotNull().Value);
        }

        protected void RegisterConverter(IDbValueConverter converter, DbType dbType) => _dbTypeConverters[dbType] = converter;
        protected void RegisterConverter(IDbValueConverter converter, TDbType dbType) => _tdbTypeConverters[dbType] = converter;
        protected void RegisterConverter(IDbValueConverter converter, TDbType tdbType, DbType dbType)
        {
            _dbTypeConverters[dbType] = _tdbTypeConverters[tdbType] = converter;
        }

        protected virtual void SetValue(TDbParameter parameter, TDbType type, object value, IDbValueConverter converter)
        {
            parameter.Value = converter.TryConvert(value, out var res) ? res : value;
        }
        protected virtual void SetValue(TDbParameter parameter, DbType type, object value, IDbValueConverter converter)
        {
            parameter.Value = converter.TryConvert(value, out var res) ? converter.ConvertBack(res) : value;
        }
        public void SetValue(TDbParameter parameter, TDbType type, object value)
        {
            parameter.VerifyArgument(nameof(parameter)).IsNotNull();
            value.VerifyArgument(nameof(value)).IsNotNull();
            if (_tdbTypeConverters.TryGetValue(type, out var converter))
            {
                /*
                 * We set the dbtype first, to allow the implementation of set value to override it whenever necessary
                 */
                parameter.DbType = converter.DbType;
                SetValue(parameter, type, value, converter);
            }
            else
            {
                parameter.Value = value;
            }
        }
        public void SetValue(TDbParameter parameter, DbType type, object value)
        {
            parameter.VerifyArgument(nameof(parameter)).IsNotNull();
            value.VerifyArgument(nameof(value)).IsNotNull();
            if (_dbTypeConverters.TryGetValue(type, out var converter))
            {
                SetValue(parameter, type, value, converter);
            }
            else
            {
                parameter.Value = value;
            }
        }
        public void SetValue(IDataParameter parameter, DbType type, object value)
        {
            if (parameter is TDbParameter dbParameter)
            {
                SetValue(dbParameter, type, value);
            }
            else
            {
                parameter.Value = value;
            }
        }
    }
}
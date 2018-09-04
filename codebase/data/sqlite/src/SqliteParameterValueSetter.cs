using System.Collections.Generic;
using System.Data;

using Axle.Data.Common;
using Axle.Data.Conversion;
using Axle.Data.Sqlite.Conversion;

#if NETFRAMEWORK
using SqliteParameter = System.Data.SQLite.SQLiteParameter;
using SqliteType      = Axle.Data.Sqlite.SqliteType;
#else
using SqliteParameter = Microsoft.Data.Sqlite.SqliteParameter;
using SqliteType      = Microsoft.Data.Sqlite.SqliteType;
#endif


namespace Axle.Data.Sqlite
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class SqliteParameterValueSetter : DbParameterValueSetter<SqliteParameter, SqliteType>
    {
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        [System.Serializable]
        #endif
        private class SqliteDbTypeEqualityComparer : IEqualityComparer<SqliteType>
        {
            public bool Equals(SqliteType x, SqliteType y) => x == y;

            public int GetHashCode(SqliteType obj) => (int) obj;
        }

        public SqliteParameterValueSetter() : base(new SqliteDbTypeEqualityComparer())
        {
            RegisterConverter(new SqliteInt16Converter());
            RegisterConverter(new SqliteInt32Converter());
            RegisterConverter(new SqliteInt64Converter());
            RegisterConverter(new SqliteSingleConverter());
            RegisterConverter(new SqliteDoubleConverter());
            RegisterConverter(new SqliteDecimalConverter());
            RegisterConverter(new SqliteTextConverter());
        }

        private void RegisterConverter<T1, T2>(SqliteDbTypeConverter<T1, T2> converter)
        {
            if (converter.RegisterAbstractDbType)
            {
                RegisterConverter(converter, converter.SqliteType, converter.DbType);
            }
            else
            {
                RegisterConverter(converter, converter.SqliteType);
            }
        }

        protected override void SetValue(SqliteParameter parameter, DbType type, object value, IDbValueConverter converter)
        {
            parameter.Value = converter.Convert(value);
            parameter.ResetDbType();
        }

        protected override void SetValue(SqliteParameter parameter, SqliteType type, object value, IDbValueConverter converter)
        {
            parameter.Value = converter.Convert(value);
        }
    }
}
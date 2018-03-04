using System.Collections.Generic;
using System.Data;

using Axle.Data.Common;
using Axle.Data.Conversion;
using Axle.Data.Sqlite.Conversion;

#if !NETSTANDARD
using SqliteParameter = System.Data.SQLite.SQLiteParameter;
using SqliteType      = Axle.Data.Sqlite.SQLiteColumnType;
#else
using SqliteParameter = Microsoft.Data.Sqlite.SqliteParameter;
using SqliteType      = Microsoft.Data.Sqlite.SqliteType;
#endif


namespace Axle.Data.Sqlite
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal sealed class SqliteParameterValueSetter : DbParameterValueSetter<SqliteParameter, SqliteType>
    {
        #if !NETSTANDARD
        [System.Serializable]
        #endif
        private class SqliteDbTypeEqualityComparer : IEqualityComparer<SqliteType>
        {
            public bool Equals(SqliteType x, SqliteType y) => x == y;

            public int GetHashCode(SqliteType obj) => (int) obj;
        }

        public SqliteParameterValueSetter() : base(new SqliteDbTypeEqualityComparer())
        {
            RegisterConverter(new SqliteInt64Converter());
            RegisterConverter(new SqliteDecimalConverter());
            RegisterConverter(new SqliteTextConverter());
        }

        private void RegisterConverter<T>(SqliteDbTypeConverter<T> converter) => RegisterConverter(converter, converter.SqliteType);

        protected override void SetValue(SqliteParameter parameter, DbType type, object value, IDbValueConverter converter)
        {
            parameter.Value = converter.Convert(value);
        }

        protected override void SetValue(SqliteParameter parameter, SqliteType type, object value, IDbValueConverter converter)
        {
            parameter.Value = converter.Convert(value);
        }
    }
}
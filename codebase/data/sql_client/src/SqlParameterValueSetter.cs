using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using Axle.Data.Common;
using Axle.Data.Conversion;
using Axle.Data.SqlClient.Conversion;


namespace Axle.Data.SqlClient
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class SqlParameterValueSetter : DbParameterValueSetter<SqlParameter, SqlDbType>
    {
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        [System.Serializable]
        #endif
        private class SqlDbTypeEqualityComparer : IEqualityComparer<SqlDbType>
        {
            public bool Equals(SqlDbType x, SqlDbType y) => x == y;

            public int GetHashCode(SqlDbType obj) => (int) obj;
        }

        public SqlParameterValueSetter() : base(new SqlDbTypeEqualityComparer())
        {
            RegisterConverter(new SqlBooleanConverter());
            RegisterConverter(new SqlByteConverter());
            RegisterConverter(new SqlInt16Converter());
            RegisterConverter(new SqlInt32Converter());
            RegisterConverter(new SqlInt64Converter());
            RegisterConverter(new SqlSingleConverter());
            RegisterConverter(new SqlDoubleConverter());
            RegisterConverter(new SqlDecimalConverter());
            RegisterConverter(new SqlMoneyConverter());
            RegisterConverter(new SqlDateTimeConverter());
            RegisterConverter(new SqlGuidConverter());
            RegisterConverter(new SqlAnsiStringFixedLengthConverter());
            RegisterConverter(new SqlAnsiStringConverter());
            RegisterConverter(new SqlStringFixedLengthConverter());
            RegisterConverter(new SqlStringConverter());
            RegisterConverter(new SqlXmlConverter());
            RegisterConverter(new SqlBinaryConverter());
        }

        private void RegisterConverter<T1, T2>(SqlDbTypeConverter<T1, T2> converter) => RegisterConverter(converter, converter.SqlType, converter.DbType);

        protected override void SetValue(SqlParameter parameter, DbType type, object value, IDbValueConverter converter)
        {
            parameter.SqlValue = converter.Convert(value);
        }

        protected override void SetValue(SqlParameter parameter, SqlDbType type, object value, IDbValueConverter converter)
        {
            parameter.SqlValue = converter.Convert(value);
        }
    }
}
using System.Collections.Generic;
using System.Data;
using Axle.Data.Common;
using Axle.Data.Conversion;
using Axle.Data.MySql.Conversion;
using MySql.Data.MySqlClient;

namespace Axle.Data.MySql
{
    internal sealed class MySqlParameterValueSetter : DbParameterValueSetter<MySqlParameter, MySqlDbType>
    {
        private class MySqlDbTypeEqualityComparer : IEqualityComparer<MySqlDbType>
        {
            public bool Equals(MySqlDbType x, MySqlDbType y) => x == y;

            public int GetHashCode(MySqlDbType obj) => (int) obj;
        }

        public MySqlParameterValueSetter() : base(new MySqlDbTypeEqualityComparer())
        {
            RegisterConverter(new MySqlBooleanConverter());
            RegisterConverter(new MySqlByteConverter());
            RegisterConverter(new MySqlInt16Converter());
            RegisterConverter(new MySqlInt32Converter());
            RegisterConverter(new MySqlInt64Converter());
            RegisterConverter(new MySqlUInt16Converter());
            RegisterConverter(new MySqlUInt32Converter());
            RegisterConverter(new MySqlUInt64Converter());
            RegisterConverter(new MySqlSingleConverter());
            RegisterConverter(new MySqlDoubleConverter());
            //RegisterConverter(new MySqlDecimalConverter());
            RegisterConverter(new MySqlDateTimeConverter());
            RegisterConverter(new MySqlGuidConverter());
            //RegisterConverter(new MySqlAnsiStringFixedLengthConverter());
            //RegisterConverter(new MySqlAnsiStringConverter());
            RegisterConverter(new MySqlStringFixedLengthConverter());
            RegisterConverter(new MySqlStringConverter());
        }

        private void RegisterConverter<T1, T2>(MySqlDbTypeConverter<T1, T2> converter) => RegisterConverter(converter, converter.MySqlDbType, converter.DbType);

        protected override void SetValue(MySqlParameter parameter, DbType type, object value, IDbValueConverter converter)
        {
            parameter.Value = converter.Convert(value);
        }

        protected override void SetValue(MySqlParameter parameter, MySqlDbType type, object value, IDbValueConverter converter)
        {
            parameter.Value = converter.Convert(value);
        }
    }
}

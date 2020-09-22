using System.Collections.Generic;
using System.Data;
using Axle.Data.Common;
using Axle.Data.Conversion;
using Axle.Data.Npgsql.Conversion;
using Npgsql;
using NpgsqlTypes;

namespace Axle.Data.Npgsql
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class NpgsqlParameterValueSetter : DbParameterValueSetter<NpgsqlParameter, NpgsqlDbType>
    {
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        [System.Serializable]
        #endif
        private class NpgsqlDbTypeEqualityComparer : IEqualityComparer<NpgsqlDbType>
        {
            public bool Equals(NpgsqlDbType x, NpgsqlDbType y) => x == y;

            public int GetHashCode(NpgsqlDbType obj) => (int) obj;
        }

        public NpgsqlParameterValueSetter() : base(new NpgsqlDbTypeEqualityComparer())
        {
            RegisterConverter(new NpgsqlBooleanConverter());
            RegisterConverter(new NpgsqlSmallintConverter());
            RegisterConverter(new NpgsqlIntegerConverter());
            RegisterConverter(new NpgsqlBigintConverter());
            RegisterConverter(new NpgsqlSingleConverter());
            RegisterConverter(new NpgsqlDoubleConverter());
            RegisterConverter(new NpgsqlMoneyConverter());
            #if NETSTANDARD || NET45_OR_NEWER
            RegisterConverter(new NpgsqlTimeSpanConverter());
            RegisterConverter(new NpgsqlTimestampTZConverter());
            RegisterConverter(new NpgsqlTimestampConverter());
            #endif
            RegisterConverter(new NpgsqlGuidConverter());
            RegisterConverter(new NpgsqlStringFixedLengthConverter());
            RegisterConverter(new NpgsqlStringConverter());
            //RegisterConverter(new NpgsqlXmlConverter());
            //RegisterConverter(new NpgsqlBinaryConverter());
        }

        private void RegisterConverter<T1, T2>(NpgsqlDbTypeConverter<T1, T2> converter)
        {
            if (converter.RegisterAbstractDbType)
            {
                RegisterConverter(converter, converter.NpgsqlType, converter.DbType);
            }
            else
            {
                RegisterConverter(converter, converter.NpgsqlType);
            }
        }

        protected override void SetValue(NpgsqlParameter parameter, DbType type, object value, IDbValueConverter converter)
        {
            parameter.NpgsqlValue = converter.Convert(value);
        }

        protected override void SetValue(NpgsqlParameter parameter, NpgsqlDbType type, object value, IDbValueConverter converter)
        {
            parameter.NpgsqlDbType = type;
            parameter.NpgsqlValue = converter.Convert(value);
        }
    }
}
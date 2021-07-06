#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;

using Axle.Data.Common;
using Axle.Data.Conversion;
using Axle.Data.Odbc.Conversion;


namespace Axle.Data.Odbc
{
    internal sealed class OdbcParameterValueSetter : DbParameterValueSetter<OdbcParameter, OdbcType>
    {
        private class OdbcTypeEqualityComparer : IEqualityComparer<OdbcType>
        {
            public bool Equals(OdbcType x, OdbcType y) => x == y;

            public int GetHashCode(OdbcType obj) => (int) obj;
        }

        public OdbcParameterValueSetter() : base(new OdbcTypeEqualityComparer())
        {
            RegisterConverter(new OdbcBooleanConverter());
            RegisterConverter(new OdbcSmallIntConverter());
            RegisterConverter(new OdbcIntConverter());
            RegisterConverter(new OdbcBigIntConverter());
            RegisterConverter(new OdbcSingleConverter());
            RegisterConverter(new OdbcDoubleConverter());
            RegisterConverter(new OdbcDecimalConverter());
            RegisterConverter(new OdbcNumericConverter());
            RegisterConverter(new OdbcSmallDateTimeConverter());
            RegisterConverter(new OdbcTimestampConverter());
            RegisterConverter(new OdbcDateTimeConverter());
            RegisterConverter(new OdbcGuidConverter());
            RegisterConverter(new OdbcAnsiTextConverter());
            RegisterConverter(new OdbcAnsiStringFixedLengthConverter());
            RegisterConverter(new OdbcAnsiStringConverter());
            RegisterConverter(new OdbcTextConverter());
            RegisterConverter(new OdbcStringFixedLengthConverter());
            RegisterConverter(new OdbcStringConverter());
            RegisterConverter(new OdbcBinaryConverter());
            RegisterConverter(new OdbcVarBinaryConverter());
        }

        private void RegisterConverter<T1, T2>(OdbcTypeConverter<T1, T2> converter)
        {
            if (converter.RegisterAbstractDbType)
            {
                RegisterConverter(converter, converter.OdbcType, converter.DbType);
            }
            else
            {
                RegisterConverter(converter, converter.OdbcType);
            }
        }

        protected override void SetValue(OdbcParameter parameter, DbType type, object value, IDbValueConverter converter)
        {
            parameter.Value = converter.Convert(value);
            parameter.ResetDbType();
        }

        protected override void SetValue(OdbcParameter parameter, OdbcType type, object value, IDbValueConverter converter)
        {
            parameter.OdbcType = type;
            parameter.Value = converter.Convert(value);
        }
    }
}
#endif
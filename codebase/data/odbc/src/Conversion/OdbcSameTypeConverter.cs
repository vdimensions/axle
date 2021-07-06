#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Data;
using System.Data.Odbc;

namespace Axle.Data.Odbc.Conversion
{
    internal abstract class OdbcSameTypeConverter<T> : OdbcTypeConverter<T, T>
    {
        protected OdbcSameTypeConverter(DbType dbType, OdbcType npgsqlDbType, bool registerAbstractDbType) 
            : base(dbType, npgsqlDbType, registerAbstractDbType) { }

        protected override T GetNotNullSourceValue(T value) => value;
        protected override T GetNotNullDestinationValue(T value) => value;

        protected override T SourceNullEquivalent => default(T);
        protected override T DestinationNullEquivalent => default(T);
    }
}
#endif
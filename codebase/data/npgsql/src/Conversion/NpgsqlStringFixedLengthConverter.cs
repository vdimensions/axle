using System.Data;

using NpgsqlTypes;


namespace Axle.Data.Npgsql.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class NpgsqlStringFixedLengthConverter : NpgsqlSameTypeConverter<string>
    {
        public NpgsqlStringFixedLengthConverter() : base(DbType.StringFixedLength, NpgsqlDbType.Char, true) { }
    }
}
using System.Data;

using NpgsqlTypes;


namespace Axle.Data.Npgsql.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal sealed class NpgsqlStringFixedLengthConverter : NpgsqlSameTypeConverter<string>
    {
        public NpgsqlStringFixedLengthConverter() : base(DbType.StringFixedLength, NpgsqlDbType.Char, true) { }
    }
}
using System.Data;

using NpgsqlTypes;


namespace Axle.Data.Npgsql.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class NpgsqlIntegerConverter : NpgsqlSameTypeConverter<int?>
    {
        public NpgsqlIntegerConverter() : base(DbType.Int32, NpgsqlDbType.Integer, true) { }
    }
}
using System.Data;

using NpgsqlTypes;


namespace Axle.Data.Npgsql.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class NpgsqlSingleConverter : NpgsqlSameTypeConverter<float?>
    {
        public NpgsqlSingleConverter() : base(DbType.Single, NpgsqlDbType.Real, true) { }
    }
}
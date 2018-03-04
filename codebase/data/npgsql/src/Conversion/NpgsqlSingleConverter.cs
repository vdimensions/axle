using System.Data;

using NpgsqlTypes;


namespace Axle.Data.Npgsql.Conversion
{
    internal sealed class NpgsqlSingleConverter : NpgsqlSameTypeConverter<float?>
    {
        public NpgsqlSingleConverter() : base(DbType.Single, NpgsqlDbType.Real) { }
    }
}
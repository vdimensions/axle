using System.Data;
using NpgsqlTypes;

namespace Axle.Data.Npgsql.Conversion
{
    internal sealed class NpgsqlSmallintConverter : NpgsqlSameTypeConverter<short?>
    {
        public NpgsqlSmallintConverter() : base(DbType.Int16, NpgsqlDbType.Smallint, true) { }
    }
}
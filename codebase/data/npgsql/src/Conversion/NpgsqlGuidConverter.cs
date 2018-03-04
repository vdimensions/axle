using System;
using System.Data;

using NpgsqlTypes;


namespace Axle.Data.Npgsql.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal sealed class NpgsqlGuidConverter : NpgsqlSameTypeConverter<Guid?>
    {
        public NpgsqlGuidConverter() : base(DbType.Guid, NpgsqlDbType.Uuid, true) { }
    }
}
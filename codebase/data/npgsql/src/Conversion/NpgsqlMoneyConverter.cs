using System.Data;

using NpgsqlTypes;


namespace Axle.Data.Npgsql.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal sealed class NpgsqlMoneyConverter : NpgsqlSameTypeConverter<decimal?>
    {
        public NpgsqlMoneyConverter() : base(DbType.Currency, NpgsqlDbType.Money, true) { }
    }
}
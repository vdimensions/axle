using System.Data;
using MySql.Data.MySqlClient;


namespace Axle.Data.MySql.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class MySqlUInt64Converter : MySqlSameTypeConverter<ulong?>
    {
        public MySqlUInt64Converter() : base(DbType.UInt64, MySqlDbType.UInt64, true) { }
    }
}
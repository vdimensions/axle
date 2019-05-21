using System.Data;

using MySql.Data.MySqlClient;


namespace Axle.Data.MySql.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class MySqlUInt32Converter : MySqlSameTypeConverter<uint?>
    {
        public MySqlUInt32Converter() : base(DbType.UInt32, MySqlDbType.UInt32, true) { }
    }
}
using System.Data;

using MySql.Data.MySqlClient;


namespace Axle.Data.MySql.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class MySqlUInt16Converter : MySqlSameTypeConverter<ushort?>
    {
        public MySqlUInt16Converter() : base(DbType.UInt16, MySqlDbType.UInt16, true) { }
    }
}
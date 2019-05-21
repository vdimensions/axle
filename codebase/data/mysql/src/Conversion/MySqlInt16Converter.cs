using System.Data;
using MySql.Data.MySqlClient;


namespace Axle.Data.MySql.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class MySqlInt16Converter : MySqlSameTypeConverter<short?>
    {
        public MySqlInt16Converter() : base(DbType.Int16, MySqlDbType.Int16, true) { }
    }
}
using System.Data;
using MySql.Data.MySqlClient;


namespace Axle.Data.MySql.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class MySqlInt32Converter : MySqlSameTypeConverter<int?>
    {
        public MySqlInt32Converter() : base(DbType.Int32, MySqlDbType.Int32, true) { }
    }
}
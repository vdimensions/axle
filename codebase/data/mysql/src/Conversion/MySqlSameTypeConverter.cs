using System.Data;
using MySql.Data.MySqlClient;


namespace Axle.Data.MySql.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal abstract class MySqlSameTypeConverter<T> : MySqlDbTypeConverter<T, T>
    {
        protected MySqlSameTypeConverter(DbType dbType, MySqlDbType mySqlDbType, bool registerAbstractDbType) : base(dbType, mySqlDbType, registerAbstractDbType) { }

        protected override T GetNotNullSourceValue(T value) => value;
        protected override T GetNotNullDestinationValue(T value) => value;

        protected override T SourceNullEquivalent => default(T);
        protected override T DestinationNullEquivalent => default(T);
    }
}
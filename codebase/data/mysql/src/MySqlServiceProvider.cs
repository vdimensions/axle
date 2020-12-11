using Axle.References;

namespace Axle.Data.MySql
{
    public sealed class MySqlServiceProvider : AbstractMySqlServiceProvider
    {
        public const string Name = "MySql.Data.MySqlClient";
        public const string Dialect = "MySQL";

        public static MySqlServiceProvider Instance => Singleton<MySqlServiceProvider>.Instance.Value;

        private MySqlServiceProvider() : base(Name, Dialect) { }
    }

}

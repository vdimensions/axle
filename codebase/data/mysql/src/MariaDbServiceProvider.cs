using Axle.References;

namespace Axle.Data.MySql
{
    public sealed class MariaDbServiceProvider : AbstractMySqlServiceProvider
    {
        public const string Name = MySqlServiceProvider.Name + "/" + Dialect;
        public const string Dialect = "MariaDB";

        public static MariaDbServiceProvider Instance => Singleton<MariaDbServiceProvider>.Instance.Value;

        private MariaDbServiceProvider() : base(Name, Dialect) { }
    }

}

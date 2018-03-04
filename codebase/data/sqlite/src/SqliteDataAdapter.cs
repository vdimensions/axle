#if NETSTANDARD
using System.Data.Common;

using Microsoft.Data.Sqlite;


namespace Axle.Data.Sqlite
{
    public sealed class SqliteDataAdapter : DbDataAdapter
    {
        internal SqliteDataAdapter(SqliteCommand _)
        {
            // POLYFILL
            throw new System.NotSupportedException();
        }
    }
}
#endif
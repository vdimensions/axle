using Axle.Verification;

namespace Axle.Data.Sqlite
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder LoadSqlite(this IApplicationBuilder builder)
        {
            return builder.VerifyArgument(nameof(builder)).IsNotNull().Value.Load<SqliteModule>();
        }
    }
}
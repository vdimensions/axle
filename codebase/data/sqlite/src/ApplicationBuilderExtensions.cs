using Axle.Verification;

namespace Axle.Data.Sqlite
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSqlite(this IApplicationBuilder builder)
        {
            builder.VerifyArgument(nameof(builder)).IsNotNull();
            return builder.ConfigureModules(m => m.Load<SqliteModule>());
        }
    }
}
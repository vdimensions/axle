using System.Diagnostics.CodeAnalysis;
using Axle.Verification;

namespace Axle.Data.Sqlite.Microsoft
{
    public static class ApplicationBuilderExtensions
    {
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public static IApplicationBuilder UseSqlite(this IApplicationBuilder app)
        {
            app.VerifyArgument(nameof(app)).IsNotNull();
            return app.ConfigureModules(m => m.Load<SqliteModule>());
        }
    }
}
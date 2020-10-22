using System.Diagnostics.CodeAnalysis;
using Axle.Application;
using Axle.Verification;

namespace Axle.Data.SQLite
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSQLite(this IApplicationBuilder app)
        {
            app.VerifyArgument(nameof(app)).IsNotNull();
            return app.ConfigureModules(m => m.Load<SQLiteModule>());
        }
    }
}
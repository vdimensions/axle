using System.Diagnostics.CodeAnalysis;
using Axle.Application;
using Axle.Verification;

namespace Axle.Data.Versioning.Migrations
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMigrations(this IApplicationBuilder app)
        {
            app.VerifyArgument(nameof(app)).IsNotNull();
            return app.ConfigureModules(m => m.Load<MigratorModule>());
        }
    }
}
using System.Diagnostics.CodeAnalysis;
using Axle.Verification;

namespace Axle.Web.AspNetCore.Cors
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class ApplicationBuilderExtensions
    {
        private static IApplicationBuilder RegisterAspNetCoreCorsModule(IApplicationBuilder app)
        {
            return app.ConfigureModules(m => m.Load<AspNetCoreCorsModule>());
        }

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public static IApplicationBuilder UseAspNetCoreCors(this IApplicationBuilder app)
        {
            app.VerifyArgument(nameof(app)).IsNotNull();
            return RegisterAspNetCoreCorsModule(app);
        }
    }
}

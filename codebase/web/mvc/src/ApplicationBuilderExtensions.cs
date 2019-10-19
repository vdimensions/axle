using System.Diagnostics.CodeAnalysis;

namespace Axle.Web.AspNetCore.Mvc
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class ApplicationBuilderExtensions
    {
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public static IApplicationBuilder UseAspNetCoreMvc(this IApplicationBuilder app) => app.ConfigureModules(m => m.Load<AspNetMvcModule>());
    }
}

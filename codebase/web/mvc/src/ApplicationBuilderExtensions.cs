using System.Diagnostics.CodeAnalysis;
using Axle.Application;

namespace Axle.Web.AspNetCore.Mvc
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class ApplicationBuilderExtensions
    {
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public static IApplicationBuilder UseAspNetCoreMvc(this IApplicationBuilder app) 
            => app.ConfigureModules(m => m.Load<AspNetCoreMvcModule>());
    }
}

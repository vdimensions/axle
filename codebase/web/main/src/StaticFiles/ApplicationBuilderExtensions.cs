using System.Diagnostics.CodeAnalysis;


namespace Axle.Web.AspNetCore.StaticFiles
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class ApplicationBuilderExtensions
    {
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public static IApplicationBuilder UseStaticFiles(this IApplicationBuilder app) => 
            app.ConfigureModules(m => m.Load<StaticFilesModule>());
    }
}

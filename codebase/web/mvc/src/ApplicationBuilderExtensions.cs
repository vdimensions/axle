using System.Diagnostics.CodeAnalysis;
using Axle.Application;
using Axle.Verification;

namespace Axle.Web.AspNetCore.Mvc
{
    /// <summary>
    /// A static class containing extension methods for enabling AspNetCoreMvc into an axle application. 
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Enables AspNetCoreMvc in the current Axle application.
        /// </summary>
        /// <param name="app">
        /// The <see cref="IApplicationBuilder"/> instance that is being configured. 
        /// </param>
        /// <returns>
        /// A reference to the <see cref="IApplicationBuilder"/> instance that is being configured.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="app"/> is <c>null</c>.
        /// </exception>
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public static IApplicationBuilder UseAspNetCoreMvc(this IApplicationBuilder app)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(app, nameof(app)));
            return app.ConfigureModules(m => m.Load<AspNetCoreMvcModule>());
        }
    }
}

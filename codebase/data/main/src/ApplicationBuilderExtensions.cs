using Axle.Verification;

namespace Axle.Data
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseData(this IApplicationBuilder app)
        {
            return app.VerifyArgument(nameof(app)).IsNotNull().Value.ConfigureModules(c => c.Load(typeof(DataModule)));
        }
    }
}

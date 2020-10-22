using Axle.Application;
using Axle.Verification;

namespace Axle.Data.DataSources
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDataSources(this IApplicationBuilder app)
        {
            return app.VerifyArgument(nameof(app)).IsNotNull().Value.ConfigureModules(c => c.Load(typeof(DataSourceModule)));
        }
    }
}

using Axle.Verification;

namespace Axle.Data.MySql
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMySql(this IApplicationBuilder app)
        {
            app.VerifyArgument(nameof(app)).IsNotNull();
            return app.ConfigureModules(m => m.Load<MySqlModule>());
        }
    }
}
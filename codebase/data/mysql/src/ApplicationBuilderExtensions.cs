using Axle.Verification;

namespace Axle.Data.MySql
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMySql(this IApplicationBuilder builder)
        {
            builder.VerifyArgument(nameof(builder)).IsNotNull();
            return builder.ConfigureModules(m => m.Load<MySqlModule>());
        }
    }
}
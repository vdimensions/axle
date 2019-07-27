using Axle.Verification;

namespace Axle.Data.Npgsql
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UsePostgreSql(this IApplicationBuilder builder)
        {
            builder.VerifyArgument(nameof(builder)).IsNotNull();
            return builder.ConfigureModules(m => m.Load<NpgsqlModule>());
        }
    }
}
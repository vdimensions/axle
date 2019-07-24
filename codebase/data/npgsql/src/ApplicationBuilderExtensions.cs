using Axle.Verification;

namespace Axle.Data.Npgsql
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UsePostgreSql(this IApplicationBuilder builder)
        {
            return builder.VerifyArgument(nameof(builder)).IsNotNull().Value.Load<NpgsqlModule>();
        }
    }
}
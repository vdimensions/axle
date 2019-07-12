using Axle.Verification;

namespace Axle.Data.MySql
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder LoadMySqlModule(this IApplicationBuilder builder)
        {
            return builder.VerifyArgument(nameof(builder)).IsNotNull().Value.Load<MySqlModule>();
        }
    }
}
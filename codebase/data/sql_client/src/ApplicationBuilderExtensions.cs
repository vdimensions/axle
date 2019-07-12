using Axle.Verification;

namespace Axle.Data.SqlClient
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder LoadSqlClientModule(this IApplicationBuilder builder)
        {
            return builder.VerifyArgument(nameof(builder)).IsNotNull().Value.Load<SqlClientModule>();
        }
    }
}
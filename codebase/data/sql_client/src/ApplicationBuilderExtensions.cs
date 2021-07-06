using System.Diagnostics.CodeAnalysis;
using Axle.Application;
using Axle.Verification;

namespace Axle.Data.SqlClient
{
    public static class ApplicationBuilderExtensions
    {
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public static IApplicationBuilder UseSqlClient(this IApplicationBuilder builder)
        {
            builder.VerifyArgument(nameof(builder)).IsNotNull();
            return builder.ConfigureModules(m => m.Load<SqlClientModule>());
        }
    }
}
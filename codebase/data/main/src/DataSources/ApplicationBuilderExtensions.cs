using Axle.Verification;

namespace Axle.Data.DataSources
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDataSources(this IApplicationBuilder builder)
        {
            return builder.VerifyArgument(nameof(builder)).IsNotNull().Value.Load(typeof(DataSourceModule));
        }
    }
}

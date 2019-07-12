using Axle.Verification;

namespace Axle.Data
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder LoadDataModule(this IApplicationBuilder builder)
        {
            return builder.VerifyArgument(nameof(builder)).IsNotNull().Value.Load(typeof(DataModule));
        }
    }
}

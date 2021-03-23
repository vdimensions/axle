namespace Axle.Web.AspNetCore.Sdk
{
    public interface IAspNetCoreConfigurer<in TOptions>
    {
        void Configure(TOptions options);
    }
    public interface IAspNetCoreConfigurer<TContext, in TOptions>
    {
        void Configure(TContext context, TOptions options);
    }
}
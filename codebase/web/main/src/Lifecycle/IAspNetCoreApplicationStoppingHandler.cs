namespace Axle.Web.AspNetCore.Lifecycle
{
    [RequiresAspNetCore]
    public interface IAspNetCoreApplicationStoppingHandler
    {
        void OnApplicationStopping();
    }
}
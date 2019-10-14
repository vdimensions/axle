namespace Axle.Web.AspNetCore.Lifecycle
{
    [RequiresAspNetCore]
    public interface IAspNetCoreApplicationStoppedHandler
    {
        void OnApplicationStopped();
    }
}
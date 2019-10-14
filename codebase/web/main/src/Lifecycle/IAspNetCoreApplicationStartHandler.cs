namespace Axle.Web.AspNetCore.Lifecycle
{
    [RequiresAspNetCore]
    public interface IAspNetCoreApplicationStartHandler
    {
        void OnApplicationStart();
    }
}
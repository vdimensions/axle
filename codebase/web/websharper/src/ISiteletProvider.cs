namespace Axle.Web.WebSharper
{
    [RequiresWebSharperSitelets]
    public interface ISiteletProvider
    {
        void RegisterSitelets(ISiteletRegistry registry);
    }
}
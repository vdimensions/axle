namespace Axle.Web.WebSharper.Sitelets
{
    [RequiresWebSharperSitelets]
    public interface ISiteletProvider
    {
        void RegisterSitelets(ISiteletRegistry registry);
    }
}
namespace Axle.Web.WebSharper.Sitelets
{
    [RequiresWebSharperSitelets]
    public interface ISiteletProvider
    {
        void RegisterSitelet(ISiteletRegistry registry);
    }
}
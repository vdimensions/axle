namespace Axle.Web.WebSharper
{
    [UtilizesWebSharperSitelets]
    public interface ISiteletProvider
    {
        void RegisterSitelets(ISiteletRegistry registry);
    }
}
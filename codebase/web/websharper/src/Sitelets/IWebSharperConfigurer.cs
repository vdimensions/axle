using WebSharper.AspNetCore;

namespace Axle.Web.WebSharper.Sitelets
{
    [RequiresWebSharperSitelets]
    public interface IWebSharperConfigurer
    {
        void Configure(WebSharperBuilder builder);
    }
}
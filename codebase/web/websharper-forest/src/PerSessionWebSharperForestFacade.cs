using Axle.Web.AspNetCore.Session;

using Microsoft.AspNetCore.Http;


namespace Axle.Web.WebSharper.Forest
{
    internal sealed class PerSessionWebSharperForestFacade : SessionScoped<WebSharperForestFacade>
    {
        public PerSessionWebSharperForestFacade(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }
    }
}
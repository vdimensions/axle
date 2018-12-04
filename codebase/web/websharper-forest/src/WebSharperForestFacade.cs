using Forest;
using Forest.Web.WebSharper;


namespace Axle.Web.WebSharper.Forest
{
    internal sealed class WebSharperForestFacade : DefaultForestFacade<WebSharperPhysicalView>
    {
        public WebSharperForestFacade(IForestContext forestContext, WebSharperPhysicalViewRenderer renderer) : base(forestContext, renderer)
        {
            Renderer = renderer;
        }

        public WebSharperPhysicalViewRenderer Renderer { get; }
    }
}
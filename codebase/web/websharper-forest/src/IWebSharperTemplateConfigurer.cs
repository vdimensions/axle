using Forest.Web.WebSharper;


namespace Axle.Web.WebSharper.Forest
{
    [RequiresWebSharperForest]
    public interface IWebSharperTemplateConfigurer
    {
        void Configure(IWebSharperTemplateRegistry registry);
    }
}
using System.Threading.Tasks;

namespace Axle.Web.AspNetCore.Mvc.ModelBinding
{
    public interface IModelResolver
    {
        Task<object> Resolve(IMvcMetadata mvcMetadata, ModelResolutionContext next);
    }
}
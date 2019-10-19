using System.Collections.Generic;
using System.Threading.Tasks;

namespace Axle.Web.AspNetCore.Mvc.ModelBinding
{
    public interface IModelResolver
    {
        Task<object> Resolve(IReadOnlyDictionary<string, object> routeData, ModelResolutionChain next);
    }
}
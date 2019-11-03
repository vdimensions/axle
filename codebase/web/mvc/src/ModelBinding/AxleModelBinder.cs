using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace Axle.Web.AspNetCore.Mvc.ModelBinding
{
    internal sealed class AxleModelBinder : IModelBinder
    {
        private readonly IModelResolver[] _resolvers;
        private readonly IModelBinder[] _binders;
        private readonly IDictionary<Type, Tuple<ModelMetadata, IModelBinder>> _metadata;

        public AxleModelBinder(IDictionary<Type, Tuple<ModelMetadata, IModelBinder>> metadata, IModelResolver[] resolvers, IModelBinder[] binders)
        {
            _metadata = metadata;
            _resolvers = resolvers;
            _binders = binders;
        }


        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ModelResolutionContext defaultResolutionContext = new ModelBinderResolutionContext(bindingContext, _metadata);
            if (_resolvers.Length > 0)
            {
                var routeData = bindingContext.ActionContext.RouteData.Values;
                var first = _resolvers[0];
                var chainedContext = _resolvers.Skip(1).Reverse().Aggregate(
                    defaultResolutionContext, 
                    (context, resolver) => new ModelResolverResolutionContext(routeData, resolver, context));
                var result = await first.Resolve(routeData, chainedContext);
                bindingContext.Result = result != null 
                    ? ModelBindingResult.Success(result) 
                    : ModelBindingResult.Failed();
            }
            else
            {
                if (_binders.Length > 0)
                {
                    await _binders[0].BindModelAsync(bindingContext);
                }
                bindingContext.Result = ModelBindingResult.Failed();
            }
        }
    }
}
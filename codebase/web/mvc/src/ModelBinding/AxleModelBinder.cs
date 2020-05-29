using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace Axle.Web.AspNetCore.Mvc.ModelBinding
{
    internal sealed class AxleModelBinder : IModelBinder
    {
        private static Uri GetUri(ModelBindingContext bindingContext)
        {
            var request = bindingContext.ActionContext.HttpContext.Request;
            var uriBuilder = new UriBuilder
            {
                Scheme = request.Scheme
            };
            if (request.Host.HasValue)
            {
                uriBuilder.Host = request.Host.Host;
                if (request.Host.Port.HasValue)
                {
                    uriBuilder.Port = request.Host.Port.Value;
                }
            }
            if (request.Path.HasValue)
            {
                uriBuilder.Path = request.Path.Value;
            }
            if (request.QueryString.HasValue)
            {
                uriBuilder.Query = request.QueryString.Value;
            }

            return new Axle.Conversion.Parsing.UriParser().Parse(uriBuilder.ToString());
        }
        
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
                var metadata = new MvcMetadata(routeData, GetUri(bindingContext));
                var chainedContext = _resolvers.Skip(1).Reverse().Aggregate(
                    defaultResolutionContext, 
                    (context, resolver) => new ModelResolverResolutionContext(metadata, resolver, context));
                var result = await first.Resolve(metadata, chainedContext);
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
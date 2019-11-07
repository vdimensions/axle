using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Axle.Web.AspNetCore.Mvc.ModelBinding
{
    internal sealed class AxleModelBinderProvider : IModelBinderProvider
    {
        private readonly IEnumerable<Type> _modelResolverTypes;
        private readonly IEnumerable<IModelResolverProvider> _modelResolverProviders;
        private readonly IEnumerable<IModelBinderProvider> _bindingProviders;

        public AxleModelBinderProvider(IEnumerable<Type> modelResolverTypes, IEnumerable<IModelResolverProvider> modelResolverProviders, IEnumerable<IModelBinderProvider> bindingProviders)
        {
            _modelResolverTypes = modelResolverTypes;
            _modelResolverProviders = modelResolverProviders;
            _bindingProviders = bindingProviders;
        }

        private ConcurrentDictionary<Type, Tuple<ModelMetadata, IModelBinder>> RegisterModelTypesForFallbackBinding(
            ModelBinderProviderContext context, Type modelType, IModelBinder[] candidateBinders)
        {
            var tuples = new ConcurrentDictionary<Type, Tuple<ModelMetadata, IModelBinder>>();
            foreach (var type in _modelResolverTypes.Union(new []{ modelType }))
            {
                if (tuples.ContainsKey(type))
                {
                    continue;
                }
                var metadata = context.MetadataProvider.GetMetadataForType(type);
                var candidateBinder = candidateBinders
                    .Where(b => (metadata.IsComplexType) && !(b is SimpleTypeModelBinder))
                    .FirstOrDefault(x => x != null);
                var binder = candidateBinder ?? context.CreateBinder(metadata);
                if (binder is PlaceholderBinder)
                {
                    continue;
                }
                tuples.AddOrUpdate(type, Tuple.Create(metadata, binder), (_, existing) => existing);
            }

            return tuples;
        }

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            var candidateBinders = _bindingProviders.Select(b => b.GetBinder(context)).Where(x => x != null).ToArray();
            var modelType = context.Metadata.ModelType;
            var tuples = RegisterModelTypesForFallbackBinding(context, modelType, candidateBinders);
            var resolverProviders = _modelResolverProviders.Select(x => x.GetModelResolver(modelType)).Where(x => x != null).ToArray();
            var binder = resolverProviders.Length == 0 
                ? tuples.TryGetValue(modelType, out var tuple) 
                    ? tuple.Item2 
                    : candidateBinders.FirstOrDefault()
                : new AxleModelBinder(tuples, resolverProviders, candidateBinders);
            //if (resolverProviders.Length == 0)
            //{
            //    return null;// candidateBinders.FirstOrDefault();
            //}
            //return new AxleModelBinder(tuples, resolverProviders, candidateBinders.ToArray());
            return binder;
        }

    }
}
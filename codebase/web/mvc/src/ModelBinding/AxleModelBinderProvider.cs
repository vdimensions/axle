using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

        private ConcurrentDictionary<Type, Tuple<ModelMetadata, IModelBinder>> RegisterModelTypesForFallbackBinding(ModelBinderProviderContext context)
        {
            var tuples = new ConcurrentDictionary<Type, Tuple<ModelMetadata, IModelBinder>>();
            foreach (var type in _modelResolverTypes)
            {
                tuples.AddOrUpdate(
                    type,
                    t =>
                    {
                        var metadata = context.MetadataProvider.GetMetadataForType(t);
                        var binder = context.CreateBinder(metadata);
                        return Tuple.Create(metadata, binder);
                    },
                    (_, existing) => existing);
            }

            return tuples;
        }

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            var tuples = RegisterModelTypesForFallbackBinding(context);

            var modelType = context.Metadata.ModelType;
            var resolverProviders = _modelResolverProviders.Select(x => x.GetModelResolver(modelType)).Where(x => x != null).ToArray();
            var candidateBinders = _bindingProviders.Select(b => b.GetBinder(context)).Where(x => x != null);
            if (resolverProviders.Length == 0)
            {
                return candidateBinders.FirstOrDefault();
            }
            return new AxleModelBinder(tuples, resolverProviders, candidateBinders.ToArray());
        }

    }
}
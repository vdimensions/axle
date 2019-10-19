using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Axle.Web.AspNetCore.Mvc.ModelBinding
{
    internal sealed class AxleModelBinderProvider : IModelBinderProvider
    {
        private readonly IEnumerable<IModelResolverProvider> _bindingConfigurers;
        private readonly IEnumerable<IModelBinderProvider> _bindingProviders;

        public AxleModelBinderProvider(IEnumerable<IModelResolverProvider> bindingConfigurers, IEnumerable<IModelBinderProvider> bindingProviders)
        {
            _bindingConfigurers = bindingConfigurers;
            _bindingProviders = bindingProviders;
        }

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            IModelBinder nextBinder = null;
            var modelType = context.Metadata.ModelType;
            var bc = _bindingConfigurers.Select(x => x.GetModelResolver(modelType)).Where(x => x != null).ToArray();
            if (bc.Length > 0)
            {
                var bp = _bindingProviders.Select(b => b.GetBinder(context)).Where(x => x != null).ToArray();
                nextBinder = new AxleModelBinder(bc, bp);
            }
            else
            {
                nextBinder = _bindingProviders.Select(bp => bp.GetBinder(context)).SingleOrDefault(x => x != null);
            }

            return nextBinder;
        }
    }
}
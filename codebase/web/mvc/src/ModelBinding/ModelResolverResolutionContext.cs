using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Axle.Verification;

namespace Axle.Web.AspNetCore.Mvc.ModelBinding
{
    internal sealed class ModelResolverResolutionContext : ModelResolutionContext
    {
        private readonly ModelResolutionContext _next;
        private readonly IModelResolver _resolver;
        private readonly IReadOnlyDictionary<string, object> _routeData;

        public ModelResolverResolutionContext(IReadOnlyDictionary<string, object> routeData, IModelResolver resolver, ModelResolutionContext next)
        {
            _routeData = routeData;
            _resolver = resolver;
            _next = next;
        }

        public override async Task<object> Resolve(Type targetType)
        {
            targetType.VerifyArgument(nameof(targetType)).IsNotNull();
            return await _resolver.Resolve(_routeData, _next) ?? await _next.Resolve(targetType);
        }
    }
}
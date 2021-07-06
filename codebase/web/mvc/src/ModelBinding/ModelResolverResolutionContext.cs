using System;
using System.Threading.Tasks;
using Axle.Verification;

namespace Axle.Web.AspNetCore.Mvc.ModelBinding
{
    internal sealed class ModelResolverResolutionContext : ModelResolutionContext
    {
        private readonly ModelResolutionContext _next;
        private readonly IModelResolver _resolver;
        private readonly IMvcMetadata _mvcMetadata;

        public ModelResolverResolutionContext(IMvcMetadata mvcMetadata, IModelResolver resolver, ModelResolutionContext next)
        {
            _mvcMetadata = mvcMetadata;
            _resolver = resolver;
            _next = next;
        }

        public override async Task<object> Resolve(Type targetType)
        {
            targetType.VerifyArgument(nameof(targetType)).IsNotNull();
            return await _resolver.Resolve(_mvcMetadata, _next) ?? await _next.Resolve(targetType);
        }
    }
}
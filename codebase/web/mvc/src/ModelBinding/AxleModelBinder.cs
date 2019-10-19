using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Axle.Web.AspNetCore.Mvc.ModelBinding
{
    internal sealed class AxleModelBinder : IModelBinder
    {
        private readonly IModelResolver[] _resolvers;
        private readonly IModelBinder[] _binders;

        public AxleModelBinder(IModelResolver[] resolvers, IModelBinder[] binders)
        {
            _resolvers = resolvers;
            _binders = binders;
        }

        private async Task<object> DoBinding(ModelBindingContext bindingContext)
        {
            for (var i = 0; i < _binders.Length; i++)
            {
                var binder = _binders[i];
                await binder.BindModelAsync(bindingContext);
                var result = bindingContext.Result.IsModelSet ? bindingContext.Result.Model : null;
                return Task.FromResult(result);
            }
            return Task.FromResult<object>(null);
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var defaultChain = new BinderModelResolutionChain(bindingContext, _binders);
            if (_resolvers.Length > 0)
            {
                var routeData = bindingContext.ActionContext.RouteData.Values;
                var first = _resolvers[0];
                var chain = _resolvers.Skip(1).Reverse().Aggregate(
                    defaultChain, 
                    (a, b) => () => new ModelBinderResolutionChain(bindingContext, a, b));
                var model = await first.Resolve(routeData, chain);
                bindingContext.Result = ModelBindingResult.Success(model);
            }
            else
            {
                await defaultChain.Proceed();
            }
        }
    }
}
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Axle.Web.AspNetCore.Mvc.ModelBinding
{
    public abstract class ModelResolutionChain
    {
        protected readonly ModelBindingContext bindingContext;

        internal ModelResolutionChain(ModelBindingContext bindingContext)
        {
            this.bindingContext = bindingContext;
        }

        public abstract Task<T> Resolve<T>();

        public abstract Task<object> Proceed();
    }

    internal sealed class ModelBinderResolutionChain : ModelResolutionChain
    {
        private readonly IModelBinder _binder;
        private readonly ModelResolutionChain _next;
        private readonly IModelResolver _nextResolver;

        public ModelBinderResolutionChain(ModelBindingContext bindingContext, ModelResolutionChain next, IModelResolver nextResolver) : base(bindingContext)
        {
            _next = next;
            _nextResolver = nextResolver;
        }

        public override Task<T> Resolve<T>()
        {
            return _nextResolver.Resolve<T>();
        }

        public override Task<object> Proceed() => _next.Proceed();
    }
}
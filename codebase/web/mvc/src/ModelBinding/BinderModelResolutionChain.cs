using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Axle.Web.AspNetCore.Mvc.ModelBinding
{
    internal sealed class BinderModelResolutionChain : ModelResolutionChain
    {
        private readonly ModelBinderProviderContext _bindingProviderContext;
        private readonly IModelBinder[] _binders;
        private readonly ConcurrentDictionary<Type, Tuple<ModelMetadata, IModelBinder>> _tuples = new ConcurrentDictionary<Type, Tuple<ModelMetadata, IModelBinder>>();

        public BinderModelResolutionChain(ModelBinderProviderContext bindingProviderContext, ModelBindingContext bindingContext, IModelBinder[] binders) : base(bindingContext)
        {
            _bindingProviderContext = bindingProviderContext;
            _binders = binders;
        }

        public override async Task<T> Resolve<T>()
        {
            var data = _tuples.GetOrAdd(
                typeof(T),
                type =>
                {
                    var metadata = _bindingProviderContext.MetadataProvider.GetMetadataForType(type);
                    var binder = _bindingProviderContext.CreateBinder(metadata);
                    return Tuple.Create(metadata, binder);
                });
            var newBindingContext = DefaultModelBindingContext.CreateBindingContext(
                bindingContext.ActionContext,
                bindingContext.ValueProvider,
                data.Item1,
                bindingInfo: null,
                bindingContext.ModelName);

            await data.Item2.BindModelAsync(newBindingContext);
            bindingContext.Result = newBindingContext.Result;
            if (newBindingContext.Result.IsModelSet)
            {
                // Setting the ValidationState ensures properties on derived types are correctly validated
                bindingContext.ValidationState[newBindingContext.Result] = new ValidationStateEntry
                {
                    Metadata = data.Item1,
                };
                return (T) newBindingContext.Result.Model;
            }
            return default(T);
        }

        public override async Task<object> Proceed()
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
    }
}
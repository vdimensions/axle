using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Axle.Verification;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Axle.Web.AspNetCore.Mvc.ModelBinding
{
    internal sealed class ModelBinderResolutionContext : ModelResolutionContext
    { 
        private readonly ModelBindingContext _bindingContext;
        private readonly IDictionary<Type, Tuple<ModelMetadata, IModelBinder>> _metadata;

        public ModelBinderResolutionContext(ModelBindingContext bindingContext, IDictionary<Type, Tuple<ModelMetadata, IModelBinder>> metadata)
        {
            _bindingContext = bindingContext;
            _metadata = metadata;
        }

        public override async Task<object> Resolve(Type targetType)
        {
            targetType.VerifyArgument(nameof(targetType)).IsNotNull();
            if (_metadata.TryGetValue(targetType, out var data))
            {
                var newBindingContext = DefaultModelBindingContext.CreateBindingContext(
                    _bindingContext.ActionContext,
                    _bindingContext.ValueProvider,
                    data.Item1,
                    null,
                    _bindingContext.ModelName);

                await data.Item2.BindModelAsync(newBindingContext);
                _bindingContext.Result = newBindingContext.Result;

                if (newBindingContext.Result.IsModelSet)
                {
                    // Setting the ValidationState ensures properties on derived types are correctly validated
                    _bindingContext.ValidationState[newBindingContext.Result] = new ValidationStateEntry
                    {
                        Metadata = data.Item1,
                    };
                    return newBindingContext.Result.Model;
                }
            }
            else
            {
                _bindingContext.Result = ModelBindingResult.Failed();
            }
            return null;
        }
    }
}
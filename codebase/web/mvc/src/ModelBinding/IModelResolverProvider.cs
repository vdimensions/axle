using System;

namespace Axle.Web.AspNetCore.Mvc.ModelBinding
{
    [RequiresAspNetMvc]
    public interface IModelResolverProvider
    {
        IModelResolver GetModelResolver(Type modelType);
    }
}
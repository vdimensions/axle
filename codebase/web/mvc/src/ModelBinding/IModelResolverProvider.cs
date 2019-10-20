using System;

namespace Axle.Web.AspNetCore.Mvc.ModelBinding
{
    public interface IModelTypeRegistry
    {
        void Register(Type type);
    }

    [RequiresAspNetMvc]
    public interface IModelResolverProvider
    {
        void RegisterTypes(IModelTypeRegistry registry);

        IModelResolver GetModelResolver(Type modelType);
    }
}
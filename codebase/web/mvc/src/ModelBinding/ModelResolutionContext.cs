using System;
using System.Threading.Tasks;

namespace Axle.Web.AspNetCore.Mvc.ModelBinding
{
    public abstract class ModelResolutionContext
    {
        internal ModelResolutionContext() { }

        public abstract Task<object> Resolve(Type targetType);
    }
}
using System;
using System.Collections.Generic;

namespace Axle.Web.AspNetCore.Mvc.ModelBinding
{
    public interface IMvcMetadata
    {
        IReadOnlyDictionary<string, object> RouteData { get; }
        Uri Uri { get; }
    }
}
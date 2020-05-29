using System;
using System.Collections.Generic;

namespace Axle.Web.AspNetCore.Mvc.ModelBinding
{
    internal sealed class MvcMetadata : IMvcMetadata
    {
        public MvcMetadata(IReadOnlyDictionary<string, object> routeData, Uri uri)
        {
            RouteData = routeData;
            Uri = uri;
        }

        public IReadOnlyDictionary<string, object> RouteData { get; }
        public Uri Uri { get; }
    }
}
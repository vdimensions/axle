using System;

using Microsoft.AspNetCore.Http;


namespace Axle.Web.AspNetCore
{
    internal sealed class AxleHttpContextAccessor : IHttpContextAccessor
    {
        private readonly object _syncRoot = new object();
        private volatile IHttpContextAccessor _accessor;

        public IHttpContextAccessor Accessor
        {
            get
            {
                var result = _accessor;
                if (result == null)
                {
                    throw new InvalidOperationException("HTTP Context is not available.");
                }
                return _accessor;
            }
            set
            {
                if (_accessor != null)
                {
                    throw new InvalidOperationException("HTTP Context Accessor already configured");
                }
                lock (_syncRoot)
                {
                    if (_accessor != null)
                    {
                        throw new InvalidOperationException("HTTP Context Accessor already configured");
                    }
                    _accessor = value;
                }
            }
        }

        public HttpContext HttpContext
        {
            get => Accessor.HttpContext;
            set => Accessor.HttpContext = value;
        }
    }
}
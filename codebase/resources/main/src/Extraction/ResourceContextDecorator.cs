using System;
using System.Collections.Generic;
using System.Globalization;

namespace Axle.Resources.Extraction
{
    public abstract class ResourceContextDecorator : IResourceContext
    {
        private readonly IResourceContext _impl;

        protected ResourceContextDecorator(IResourceContext impl)
        {
            _impl = impl;
        }

        /// <inheritdoc />
        public virtual ResourceInfo Extract(string name) => _impl.Extract(name);

        /// <inheritdoc />
        public virtual IEnumerable<ResourceInfo> ExtractAll(string name) => _impl.ExtractAll(name);

        /// <inheritdoc />
        public virtual string Bundle => _impl.Bundle;

        /// <inheritdoc />
        public virtual Uri Location => _impl.Location;

        /// <inheritdoc />
        public virtual CultureInfo Culture => _impl.Culture;

        /// <inheritdoc />
        public virtual IResourceExtractor Extractor => _impl.Extractor;

        /// <inheritdoc />
        public virtual IResourceContext Next => _impl.Next;
    }
}
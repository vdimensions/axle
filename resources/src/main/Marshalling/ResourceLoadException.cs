using System;


namespace Axle.Resources.Marshalling
{
    public partial class ResourceLoadException : ResourceMarshallingException
    {
        public ResourceLoadException(string name, string message, Exception inner) : base(name, message, inner) { }
        public ResourceLoadException(string name, string message) : base(name, message) { }
        public ResourceLoadException(string name, Exception inner) : base(name, inner) { }
        public ResourceLoadException(string name) : base(name) { }
    }
}
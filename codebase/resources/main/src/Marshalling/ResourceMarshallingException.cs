using System;


namespace Axle.Resources.Marshalling
{
    public partial class ResourceMarshallingException : ResourceException
    {
        public ResourceMarshallingException(string name, string message) : this(name, message, null) { }
        public ResourceMarshallingException(string name, string message, Exception inner)
            : base(string.Format("An error ocurred while umarshalling resource '{0}'{1} .{2}",
                name, message != null ? $" - {message}" : string.Empty, inner != null ? " See inner exception for details." : string.Empty), inner) { }
        public ResourceMarshallingException(string name) : this(name, null, null) { }
        public ResourceMarshallingException(string name, Exception inner) : this(name, null, inner) { }
    }
}
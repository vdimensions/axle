using System;
using System.Globalization;


namespace Axle.Resources
{
    public sealed partial class ResourceNotFoundException : ResourceException
    {
        public ResourceNotFoundException() { }
        public ResourceNotFoundException(string message) : base(message) { }
        public ResourceNotFoundException(string message, Exception inner) : base(message, inner) { }
        public ResourceNotFoundException(string resourceName, string bundleName, CultureInfo culture) : this(resourceName, bundleName, culture, null) { }
        public ResourceNotFoundException(string resourceName, string bundleName, CultureInfo culture, Exception inner) : this(
                string.Format("Unable to locate resource '{0}' in bundle '{1}' for culture '{2}'.",  resourceName, bundleName, culture.DisplayName), 
                inner) { }
    }
}
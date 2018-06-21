using System;
#if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
using System.Runtime.Serialization;
#endif


namespace Axle.Core.DependencyInjection
{
    #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
    [Serializable]
    #endif
    public class DuplicateDependencyDefinitionException : DependencyRegistrationException
    {
        public DuplicateDependencyDefinitionException() { }
        public DuplicateDependencyDefinitionException(string message) : base(message) { }
        public DuplicateDependencyDefinitionException(string message, Exception inner) : base(message, inner) { }
        public DuplicateDependencyDefinitionException(Type type, string name) : this(type, name, null) { }
        public DuplicateDependencyDefinitionException(Type type, string name, Exception inner) : this(
            string.Format("A dependency {0}of the provided type `{1}` is already defined in this container.", 
                string.IsNullOrEmpty(name) ? string.Empty : $"with the given name '{name}' and ",
                type.FullName), 
            inner) { }
        #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
        protected DuplicateDependencyDefinitionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endif
    }
}
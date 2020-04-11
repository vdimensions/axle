using System;

namespace Axle.Modularity
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public class ModuleDefinitionException : Exception
    {
        public ModuleDefinitionException(Type moduleType) : this(moduleType, null) { }
        public ModuleDefinitionException(Type moduleType, Exception inner)
            : this(
                string.Format("An error occurred while inferring the definition for module `{0}`", moduleType.AssemblyQualifiedName), 
                inner) { }

        private ModuleDefinitionException(string message, Exception inner) 
            : base(message, inner) { }

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        protected ModuleDefinitionException(
                System.Runtime.Serialization.SerializationInfo info, 
                System.Runtime.Serialization.StreamingContext context) 
            : base(info, context) { }
        #endif
    }
}

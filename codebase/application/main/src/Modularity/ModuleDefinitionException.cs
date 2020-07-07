using System;

namespace Axle.Modularity
{
    /// <summary>
    /// An exception that is thrown when an application module is discovered but cannot be made usable by the
    /// module loader. For example, if an abstract type has been passed for initialization, or a module attribute is
    /// incorrectly applied (like when a lifecycle method indicator attribute placed on a method with incompatible
    /// signature).
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public class ModuleDefinitionException : Exception
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ModuleDefinitionException"/> class.
        /// </summary>
        /// <param name="moduleType">
        /// The <see cref="Type"/> of the module that failed to load. 
        /// </param>
        public ModuleDefinitionException(Type moduleType) : this(moduleType, null) { }
        /// <summary>
        /// Creates a new instance of the <see cref="ModuleDefinitionException"/> class.
        /// </summary>
        /// <param name="moduleType">
        /// The <see cref="Type"/> of the module that failed to load. 
        /// </param>
        /// <param name="inner">
        /// The <see cref="Exception"/> cause for the current <see cref="ModuleDefinitionException"/>. 
        /// </param>
        public ModuleDefinitionException(Type moduleType, Exception inner)
            : this(
                string.Format("An error occurred while inferring the definition for module `{0}`", moduleType.AssemblyQualifiedName), 
                inner) { }

        private ModuleDefinitionException(string message, Exception inner) 
            : base(message, inner) { }

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleDefinitionException"/> class with serialized data.
        /// </summary>
        protected ModuleDefinitionException(
                System.Runtime.Serialization.SerializationInfo info, 
                System.Runtime.Serialization.StreamingContext context) 
            : base(info, context) { }
        #endif
    }
}

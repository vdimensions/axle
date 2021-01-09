using System;
using System.Collections.Generic;
#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
using System.Runtime.Serialization;
#endif

using Axle.Verification;


namespace Axle.DependencyInjection
{
    /// <summary>
    /// An exception that is thrown by an <see cref="IDependencyContainer"/> when resolving an object instance in case
    /// multiple objects can be used to satisfy a particular dependency.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [Serializable]
    #endif
    public class AmbiguousDependencyException : DependencyResolutionException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AmbiguousDependencyException"/> class using the provided
        /// parameters.
        /// </summary>
        /// <param name="type">
        /// The type of the dependency that failed to resolve.
        /// </param>
        /// <param name="name">
        /// The name of the dependency that failed to resolve.
        /// </param>
        /// <param name="candidates">
        /// A collection of <see cref="DependencyCandidate"/> objects that represent the conflicting dependencies.
        /// </param>
        public AmbiguousDependencyException(Type type, string name, IEnumerable<DependencyCandidate> candidates)
            : base(type, name, "There are multiple candidates that could satisfy the dependency. ", null)
        {
            Candidates = candidates.VerifyArgument(nameof(candidates)).IsNotNullOrEmpty().Value;
        }

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
        /// <inheritdoc />
        protected AmbiguousDependencyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endif

        /// <summary>
        /// A collection of <see cref="DependencyCandidate"/> objects that represent the conflicting dependencies.
        /// </summary>
        public IEnumerable<DependencyCandidate> Candidates { get; }
    }
}
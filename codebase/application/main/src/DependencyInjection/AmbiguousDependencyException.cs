using System;
using System.Collections.Generic;
#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Runtime.Serialization;
#endif

using Axle.Verification;


namespace Axle.DependencyInjection
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public class AmbiguousDependencyException : DependencyResolutionException
    {
        public AmbiguousDependencyException(Type type, string name, IEnumerable<DependencyCandidate> candidates)
            : base(type, name, "There are multiple candidates that could satisfy the dependency. ", null)
        {
            Candidates = candidates.VerifyArgument(nameof(candidates)).IsNotNullOrEmpty().Value;
        }

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK

        /// <inheritdoc />
        protected AmbiguousDependencyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endif

        public IEnumerable<DependencyCandidate> Candidates { get; }
    }
}
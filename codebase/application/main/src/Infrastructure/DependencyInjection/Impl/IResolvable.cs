//using System;
//using System.Collections.Generic;
//
//using Axle.References;
//
//
//namespace Axle.Core.Infrastructure.DependencyInjection.Impl
//{
//    internal interface IResolvable : IReference, IResolutionCandidate
//    {
//        /// <summary>
//        /// Marks the current <see cref="IResolvable">resolvable</see> as ready for injection.
//        /// </summary>
//        /// <param name="instance">The dependency value that can be injected.</param>
//        /// <returns></returns>
//        /// <exception cref="InvalidOperationException">
//        /// <see cref="IsInjectable"/> is already set to <c>true</c>.
//        /// </exception>
//        void MarkInjectable(object instance);
//        void MarkResolved();
//
//        bool IsResolved { get; }
//        bool IsInjectable { get; }
//        IEnumerable<Recepie> Recepies { get; }
//    }
//}
//using System;
//using System.Collections.Generic;
//
//
//namespace Axle.Core.Infrastructure.DependencyInjection.Sdk
//{
//    /// <summary>
//    /// An interface representing a dependency. It contains all the information needed by a <see cref="Container">dependency container</see>
//    /// to identify and resolve a dependency object.
//    /// </summary>
//    public interface IDependency : IEquatable<IDependency>
//    {
//        /// <summary>
//        /// Gets a collection of <see cref="Attribute">attributes</see> applied to the dependency's injection target.
//        /// <para>
//        /// For example, if the injection target is a property, then the attributes defined in the property's declaration are returned.
//        /// </para>
//        /// <para>
//        /// If the injection target is an argument to a constructor/method, then the attributes applied to that argument are returned.
//        /// </para>
//        /// <para>
//        /// If the dependency represents type and name(s) that were directly requested from a <see cref="Container">container</see> 
//        /// (as trough a call to <see cref="Container.Resolve(System.Type)" /> -- thus no particular injection target is present), 
//        /// then empty collection is returned.
//        /// </para>
//        /// </summary>
//        /// <returns>
//        /// A collection of <see cref="Attribute">attributes</see> applied to the dependency's injection target.
//        /// </returns>
//        IEnumerable<Attribute> GetAttributes();
//
//        /// <summary>
//        /// The <see cref="Type">type</see> of the dependency object
//        /// </summary>
//        Type RequestedType { get; }
//
//        /// <summary>
//        /// The name explicitly requested for injection. If this value is <c>null</c>, 
//        /// an attempt to resolve the dependency using the <see cref="InferredName">inferred name</see> will be made before resolving the default candidate.
//        /// </summary>
//        string DeclaredName { get; }
//
//        /// <summary>
//        /// The interred name for the dependency. Usually, this is the name of the property, or the constructor argument that is to be injected.
//        /// </summary>
//        string InferredName { get; }
//
//        /// <summary>
//        /// A <see cref="bool">boolean</see> value that indicates whether the current <see cref="IDependency">dependency</see> is optional.
//        /// Dependency resolution for objects that have unsatisfied any or all of their optional dependencies will still succeed. 
//        /// </summary>
//        bool IsOptional { get; }
//    }
//}
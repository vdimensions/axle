//using System;
//using System.ComponentModel;
//using System.Diagnostics;
//
//
//namespace Axle.Core.Infrastructure.DependencyInjection
//{
//    /// <summary>
//    /// An attribute that is used in conjunction with the <see cref="InjectAttribute">inject attribute</see>. Marks a
//    /// property or a constructor argument as optional, which would instruct a <see cref="Container">dependency container</see> 
//    /// to proceed with dependency resolution if a value for the annotated target cannot be supplied.
//    /// </summary>
//    /// <seealso cref="InjectAttribute"/>
//    /// <seealso cref="IDependency"/>
//    /// <seealso cref="Container"/>
//    [AttributeUsage(AttributeTargets.Property|AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
//    public sealed class OptionalAttribute : Attribute
//    {
//        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//        private readonly bool _value;
//
//        /// <summary>
//        /// Creates a new instance of the <see cref="OptionalAttribute" /> class.
//        /// </summary>
//        public OptionalAttribute() : this(true) { }
//
//        /// <summary>
//        /// Creates a new instance of the <see cref="OptionalAttribute" /> class.
//        /// </summary>
//        /// <param name="value">
//        /// A <see cref="bool">boolean</see> indicating whether the annotated member should be treated as optional
//        /// if dependency injection occurs.
//        /// </param>
//        public OptionalAttribute(bool value) => _value = value;
//
//        /// <summary>
//        /// A <see cref="bool">boolean</see> indicating whether the annotated member should be treated as optional
//        /// if dependency injection occurs.
//        /// </summary>
//        /// <remarks>The default value is <c>true</c>.</remarks>
//        [DefaultValue(true)]
//        public bool Value => _value;
//    }
//}
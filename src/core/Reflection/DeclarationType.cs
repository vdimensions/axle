﻿using System;
using System.Reflection;


namespace Axle.Reflection
{
    /// <summary>
    /// An enumeration representing the possible declaration types of a class member.
    /// </summary>
    /// <remarks>
    /// Multiple values can be combined, as the enumeration has the <see cref="FlagsAttribute"/> specified.
    /// </remarks>
    [Serializable]
    [Flags]
    //[Maturity(CodeMaturity.Stable)]
    public enum DeclarationType : byte
    {
        /// <summary>
        /// No valid declaration type is specified.
        /// </summary>
        None = 0,
        /// <summary>
        /// Used for instance members of a class.
        /// </summary>
        Instance = 1,
        /// <summary>
        /// Used for static members of a class.
        /// </summary>
        /// <seealso cref="MethodBase.IsStatic"/>
        /// <seealso cref="FieldInfo.IsStatic"/>
        Static = (1 << 1),
        /// <summary>
        /// The member is abstract.
        /// </summary>
        /// <seealso cref="MethodBase.IsAbstract"/>
        Abstract = (1 << 2),
        /// <summary>
        /// The member is virtual.
        /// </summary>
        /// <seealso cref="MethodBase.IsVirtual"/>
        Virtual = (1 << 3),
        /// <summary>
        /// The member is overriding another member defined up in the class hierarchy.
        /// </summary>
        Override = (1 << 4),
        /// <summary>
        /// Determines that only a member of the same kind with exactly the same signature is hidden in the derived class.
        /// </summary>
        /// <seealso cref="MethodBase.IsHideBySig"/>
        HideBySig = (1 << 5),
        /// <summary>
        /// The member is sealed; it cannot be overriden. 
        /// </summary>
        /// <seealso cref="MethodBase.IsFinal"/>
        Sealed = (1 << 6)
    }
}
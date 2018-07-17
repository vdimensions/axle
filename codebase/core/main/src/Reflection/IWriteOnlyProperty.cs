﻿namespace Axle.Reflection
{
    /// <summary>
    /// An interface representing a reflected write-only property member.
    /// </summary>
    /// <seealso cref="System.Reflection.PropertyInfo"/>
    public interface IWriteOnlyProperty : IProperty, IWriteableMember { }
}
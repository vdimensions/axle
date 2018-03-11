using System.Collections.Generic;


namespace Axle.Reflection
{
    public interface IAccessible
    {
        IAccessor FindAccessor(AccessorType accessorType);

        IEnumerable<IAccessor> Accessors { get; }
    }
}
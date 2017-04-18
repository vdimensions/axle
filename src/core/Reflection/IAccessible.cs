using System.Collections.Generic;


namespace Axle.Reflection
{
    //[Maturity(CodeMaturity.Stable)]
    public interface IAccessible
    {
        IAccessor FindAccessor(AccessorType accessorType);

        IEnumerable<IAccessor> Accessors { get; }
    }
}
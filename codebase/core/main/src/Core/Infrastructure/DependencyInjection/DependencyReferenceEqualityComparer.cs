using System;


namespace Axle.Core.Infrastructure.DependencyInjection
{
    public sealed class DependencyReferenceEqualityComparer : AbstractEqualityComparer<IDependencyReference>
    {
        protected override bool DoEquals(IDependencyReference x, IDependencyReference y)
        {
            return (x.Type == y.Type) && StringComparer.Ordinal.Equals(x.Name, y.Name);
        }

        protected override int DoGetHashCode(IDependencyReference obj) { return obj.GetHashCode(); }
    }
}
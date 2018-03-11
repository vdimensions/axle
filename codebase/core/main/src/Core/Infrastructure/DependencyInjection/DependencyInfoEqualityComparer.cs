using System;

using Axle.Extensions.Object;


namespace Axle.Core.Infrastructure.DependencyInjection
{
    public sealed class DependencyInfoEqualityComparer : AbstractEqualityComparer<DependencyInfo>
    {
        protected override bool DoEquals(DependencyInfo x, DependencyInfo y)
        {
            var ordinalStringComparer = StringComparer.Ordinal;
            return x.Type == y.Type 
                && ordinalStringComparer.Equals(x.MemberName, y.MemberName)
                && ordinalStringComparer.Equals(x.DependencyName, y.DependencyName);
        }

        protected override int DoGetHashCode(DependencyInfo obj) => obj.CalculateHashCode(obj.Type, obj.MemberName, obj.DependencyName);
    }
}
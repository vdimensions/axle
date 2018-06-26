//using System;
//
//using Axle.Extensions.Object;
//
//
//namespace Axle.Core.Infrastructure.DependencyInjection.Sdk
//{
//    public class DependencyEqualityComparer : AbstractEqualityComparer<IDependency>
//    {
//        protected override bool DoEquals(IDependency x, IDependency y)
//        {
//            var cmp = StringComparer.Ordinal;
//            return x.RequestedType == y.RequestedType
//                && cmp.Equals(x.DeclaredName, y.DeclaredName)
//                && cmp.Equals(x.InferredName, y.InferredName);
//        }
//
//        protected override int DoGetHashCode(IDependency obj) => obj.CalculateHashCode(obj.RequestedType, obj.DeclaredName, obj.InferredName);
//    }
//}
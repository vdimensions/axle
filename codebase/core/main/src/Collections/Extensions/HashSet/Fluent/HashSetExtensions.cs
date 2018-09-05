#if NETSTANDARD || NET35_OR_NEWER
using System;
using System.Collections.Generic;


namespace Axle.Collections.Extensions.HashSet.Fluent
{
    /// <summary>
    /// A static class to contain extension methods that allow manipulating <see cref="HashSet{T}"/> collection
    /// instances using the fluent interface pattern. 
    /// </summary>
    public static class HashSetExtensions
    {
        public static TSet FluentIntersectWith<TSet, T>(this TSet set, IEnumerable<T> other) where TSet: HashSet<T>
        {
            if (set == null)
            {
                throw new ArgumentNullException(nameof(set));
            }
            set.IntersectWith(set);
            return set;
        }

        public static TSet FluentExceptWith<TSet, T>(this TSet set, IEnumerable<T> other) where TSet : HashSet<T>
        {
            if (set == null)
            {
                throw new ArgumentNullException(nameof(set));
            }
            set.ExceptWith(set);
            return set;
        }

        public static TSet FluentSymmetricExceptWith<TSet, T>(this TSet set, IEnumerable<T> other) where TSet : HashSet<T>
        {
            if (set == null)
            {
                throw new ArgumentNullException(nameof(set));
            }
            set.SymmetricExceptWith(set);
            return set;
        }

        public static TSet FluentUnionWith<TSet, T>(this TSet set, IEnumerable<T> other) where TSet: HashSet<T>
        {
            if (set == null)
            {
                throw new ArgumentNullException(nameof(set));
            }
            set.UnionWith(set);
            return set;
        }
    }
}
#endif
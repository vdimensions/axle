using System.Collections;
using System.Collections.Generic;

using Axle.Collections.Generic;


namespace Axle.Linq
{
    public static partial class SequenceExtensions
    {
        #region : Collection :: Linq(...) :
        public static Sequence<IList<T>, T> AsSequence<T>(this T[] @this) { return new Sequence<IList<T>, T>(@this); }
        public static Sequence<IEnumerable<T>, T> AsSequence<T>(this IEnumerable<T> @this)
        {
            return new Sequence<IEnumerable<T>, T>(@this, @this is System.Collections.ObjectModel.ReadOnlyCollection<T>, false);
        }
        public static Sequence<ICollection<T>, T> AsSequence<T>(this ICollection<T> @this)
        {
            return new Sequence<ICollection<T>, T>(@this, @this is System.Collections.ObjectModel.ReadOnlyCollection<T>, false);
        }
        //public static Sequence<Axle.Collections.ReadOnly.IReadOnlyCollection<T>, T> Linq<T>(this Axle.Collections.ReadOnly.IReadOnlyCollection<T> @this)
        //{
        //    return new Sequence<Axle.Collections.ReadOnly.IReadOnlyCollection<T>, T>(@this, true, false);
        //}
        //public static Sequence<Axle.Collections.ReadOnly.ReadOnlyList<T>, T> Linq<T>(this Axle.Collections.ReadOnly.ReadOnlyList<T> @this)
        //{
        //    return new Sequence<Axle.Collections.ReadOnly.ReadOnlyList<T>, T>(@this, true, false);
        //}
        public static Sequence<IList<T>, T> AsSequence<T>(this IList<T> @this)
        {
            return new Sequence<IList<T>, T>(@this, @this is System.Collections.ObjectModel.ReadOnlyCollection<T>, false);
        }
        public static Sequence<IList<T>, T> AsSequence<T>(this List<T> @this)
        {
            return new Sequence<IList<T>, T>(@this);
        }
        //public static Sequence<IDictionary<TKey, TValue>, KeyValuePair<TKey, TValue>> Linq<TKey, TValue>(this IDictionary<TKey, TValue> @this)
        //{
        //    return new Sequence<IDictionary<TKey, TValue>, KeyValuePair<TKey, TValue>>(@this, @this is Axle.Collections.ReadOnly.IReadOnlyDictionary<TKey, TValue>, false);
        //}
        //public static Sequence<IDictionary<TKey, TValue>, KeyValuePair<TKey, TValue>> Linq<TKey, TValue>(this Dictionary<TKey, TValue> @this)
        //{
        //    return new Sequence<IDictionary<TKey, TValue>, KeyValuePair<TKey, TValue>>(@this, @this is Axle.Collections.ReadOnly.IReadOnlyDictionary<TKey, TValue>, false);
        //}
        //public static Sequence<Axle.Collections.ReadOnly.ReadOnlyDictionary<TKey, TValue>, KeyValuePair<TKey, TValue>> Linq<TKey, TValue>(
        //    this Axle.Collections.ReadOnly.ReadOnlyDictionary<TKey, TValue> @this)
        //{
        //    return new Sequence<Axle.Collections.ReadOnly.ReadOnlyDictionary<TKey, TValue>, KeyValuePair<TKey, TValue>>(@this, true, false);
        //}


        public static Sequence<GenericEnumerable<object>, object> AsSequence(this ICollection @this)
        {
            return new Sequence<GenericEnumerable<object>, object>(new GenericEnumerable<object>(@this));
        }
        public static Sequence<GenericEnumerable<T>, T> AsSequence<T>(this ICollection @this)
        {
            return new Sequence<GenericEnumerable<T>, T>(new GenericEnumerable<T>(@this));
        }
        public static Sequence<GenericList<object>, object> AsSequence(this IList @this)
        {
            return new Sequence<GenericList<object>, object>(new GenericList<object>(@this));
        }
        public static Sequence<GenericList<T>, T> AsSequence<T>(this IList @this)
        {
            return new Sequence<GenericList<T>, T>(new GenericList<T>(@this));
        }
        #endregion
    }
}

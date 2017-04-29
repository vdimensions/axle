using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Axle.Collections.Generic;
using Axle.Collections.Variant;


namespace Axle.Linq
{
    public static partial class Collection
    {
        

        #region : Collection :: Contravariate(...) :
        public static Sequence<IEnumerable<T>, T> Contravariate<T, TBase>(this Sequence<IEnumerable<TBase>, TBase> @this) where T : TBase
        {
            var current = @this.Value;
            return (current as IEnumerable<T> ?? new VariantEnumerable<TBase, T>(current)).AsSequence();
        }
        public static Sequence<IList<T>, T> Contravariate<T, TBase>(this Sequence<IList<TBase>, TBase> @this) where T : TBase
        {
            var current = @this.Value;
            return AsSequence(current as IList<T> ?? new VariantList<TBase, T>(current, item => (T)item, item => (TBase) item));
        }
        //public static Sequence<Axle.Collections.ReadOnly.ReadOnlyList<T>, T> Contravariate<T, TBase>(this Sequence<Axle.Collections.ReadOnly.IReadOnlyList<TBase>, TBase> @this) where T : TBase
        //{
        //    var current = @this.Value;
        //    var readOnly = current as Axle.Collections.ReadOnly.ReadOnlyList<T>;
        //    return readOnly != null
        //        ? readOnly.Linq()
        //        : new VariantList<TBase, T>(current, item => (T)item, item => (TBase)item).Linq().AsReadOnly();
        //}

        public static Sequence<ICollection<T>, T> Contravariate<T, TBase>(this Sequence<ICollection<TBase>, TBase> @this) where T : TBase
        {
            var current = @this.Value;
            return AsSequence(new VariantCollection<TBase, T>(current, item => (T) item, item => item));
        }
        #endregion

        #region : Collection :: Covariate(...) :
        public static Sequence<IEnumerable<TBase>, TBase> Covariate<T, TBase>(this Sequence<IEnumerable<T>, T> @this) where T : TBase
        {
            var current = @this.Value;
            return AsSequence(current as IEnumerable<TBase> ?? new VariantEnumerable<T, TBase>(current));
        }
        public static Sequence<ICollection<TBase>, TBase> Covariate<T, TBase>(this Sequence<ICollection<T>, T> @this) where T : TBase
        {
            var current = @this.Value;
            return AsSequence(new VariantCollection<T, TBase>(current, item => item, item => (T) item));
        }
        public static Sequence<IList<TBase>, TBase> Covariate<T, TBase>(this Sequence<IList<T>, T> @this) where T : TBase
        {
            var current = @this.Value;
            return AsSequence(current as IList<TBase> ?? new VariantList<T, TBase>(current, item => item, item => (T) item));
        }
        //public static Sequence<Axle.Collections.ReadOnly.ReadOnlyList<TBase>, TBase> Covariate<T, TBase>(this Sequence<Axle.Collections.ReadOnly.IReadOnlyList<T>, T> @this) where T : TBase
        //{
        //    var current = @this.Value;
        //    var readOnly = current as Axle.Collections.ReadOnly.ReadOnlyList<TBase>;
        //    return readOnly != null
        //        ? readOnly.Linq()
        //        : new VariantList<T, TBase>(current, item => item, item => (T)item).Linq().AsReadOnly();
        //}
        #endregion : Collection :: Covariate(...) :

        #region : Collection :: Flatten (...) :
        public static Sequence<IEnumerable<T>, T> Flatten<T>(this Sequence<IEnumerable<IEnumerable<T>>, IEnumerable<T>> @this)
        {
            return AsSequence(Axle.Extensions.Collections.CollectionExtensions.Flatten(@this.Value));
        }
        #endregion

        #region : Collection :: Union (...) :
        public static Sequence<IEnumerable<T>, T> Union<T>(this Sequence<IEnumerable<T>, T> @this, T value)
        {
            return AsSequence(Enumerable.Union(@this.Value, new[] {value}));
        }
        #endregion
    }
}

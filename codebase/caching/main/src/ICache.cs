using System;


namespace Axle.Caching
{
    /// <summary>
    /// An interface representing a cache.
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// Removes all the stored objects within the current <see cref="ICache"/> instance.
        /// </summary>
        void Evict();
        
        /// <summary>
        /// Removes any cached object corresponding to the provided <paramref name="key"/>.
        /// </summary>
        /// <param name="key">
        /// The key of the cached object to be removed.
        /// </param>
        /// <returns>
        /// A <see cref="bool">boolean</see> value indicating whether the object was removed as a result of this
        /// operation.
        /// <para>
        /// A possible cause for returning <c>false</c> is that the object was already removed from a previous call to
        /// the <see cref="Delete"/> method.
        /// </para>
        /// <para>
        /// It could also indicate that the current <see cref="ICache"/> implementation has an expiration policy
        /// and the entry corresponding to the provided <paramref name="key"/> was deleted as a result of enforcing that
        /// policy.
        /// </para>
        /// </returns>
        bool Delete(object key);

        /// <summary>
        /// Adds a <paramref name="value"/> to the current <see cref="ICache"/> instance under the specified
        /// <paramref name="key"/>. Existing values for the same key will be silently overwritten.
        /// </summary>
        /// <param name="key">
        /// The key that will be used to access the stored <paramref name="value"/>.
        /// </param>
        /// <param name="value">
        /// The value to store in the current <see cref="ICache"/> instance.
        /// </param>
        /// <returns>
        /// A reference to the current <see cref="ICache"/> object.
        /// </returns>
        ICache Add(object key, object value);

        /// <summary>
        /// Retrieves the value for a given <paramref name="key"/> if it is present in the current <see cref="ICache"/>
        /// instance, or stores and returns the provided by the <paramref name="valueToAdd"/> parameter object.
        /// </summary>
        /// <param name="key">
        /// The key to retrieve or create a value for.
        /// </param>
        /// <param name="valueToAdd">
        /// An object that will be permanently stored in the cache for the requested <paramref name="key"/> in case
        /// the current <see cref="ICache"/> does not have a value for it.
        /// </param>
        /// <returns>
        /// The cached object for the specified <paramref name="key"/>, which was either an already stored value in the
        /// cache, otherwise the <paramref name="valueToAdd"/> object.  
        /// </returns>
        object GetOrAdd(object key, object valueToAdd);
        /// <summary>
        /// Retrieves the value for a given <paramref name="key"/> if it is present in the current <see cref="ICache"/>
        /// instance, or creates a new value for the same key by invoking the provided <paramref name="valueFactory"/>.
        /// </summary>
        /// <param name="key">
        /// The key to retrieve or create a value for.
        /// </param>
        /// <param name="valueFactory">
        /// A delegate that will be invoked if the current <see cref="ICache"/> does not contain a value for the
        /// specified <paramref name="key"/>. The resulting object will be permanently stored in the cache for the
        /// requested <paramref name="key"/>.
        /// </param>
        /// <returns>
        /// The cached object for the specified <paramref name="key"/>, which was either an already stored value in the
        /// cache, or the result of calling the <paramref name="valueFactory"/> delegate.  
        /// </returns>
        object GetOrAdd(object key, Func<object, object> valueFactory);

        /// <summary>
        /// An indexer used to get or set values in the cache.
        /// </summary>
        /// <param name="key">
        /// The key to get or set a value for.
        /// </param>
        object this[object key] { get; set; }
    }
}
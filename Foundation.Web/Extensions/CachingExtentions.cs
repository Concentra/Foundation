using System;
using System.Web.Caching;

namespace Foundation.Web.Extensions
{
    /// <summary>
    /// Caching service managed the system cache.
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Gets the particular item from cache but doesn't set if item is null.
        /// </summary>
        /// <typeparam name="T">the object of type T that will be returned from the cache.</typeparam>
        /// <param name="cacheId">The cache id.</param>
        /// <param name="type">The type of the object.</param>
        /// <returns>the object of type T or null if item is missing</returns>
        T Get<T>(string cacheId, CacheType type) where T : class, IFlushable;

        /// <summary>
        /// Gets the item from the cache and also sets the item using a query if item is absent.
        /// </summary>
        /// <typeparam name="T">the object of type T that will be returned from the cache.</typeparam>
        /// <param name="cacheId">The cache id.</param>
        /// <param name="type">The type of the object.</param>
        /// <param name="getItemCallback">The function the will query the object and set the cache.</param>
        /// <returns>Return the object of time T either from the cache or from the callback query. </returns>
        T Get<T>(string cacheId, CacheType type, Func<T> getItemCallback) where T : class, IFlushable;

        /// <summary>
        /// Gets the item from the cache and also sets the item using a query if item is absent.
        /// </summary>
        /// <typeparam name="T">the object of type T that will be returned from the cache.</typeparam>
        /// <param name="cacheId">The cache id.</param>
        /// <param name="type">The type of the object.</param>
        /// <param name="getItemCallback">The function the will query the object and set the cache.</param>
        /// <param name="priority">The priority by which to cache the item if it needs to be set</param>
        /// <returns>Return the object of time T either from the cache or from the callback query. </returns>
        T Get<T>(string cacheId, CacheType type, Func<T> getItemCallback, CacheItemPriority priority) where T : class, IFlushable;

        /// <summary>
        /// Gets the item from the cache and also sets the item using a query if item is absent.
        /// </summary>
        /// <typeparam name="T">the object of type T that will be returned from the cache.</typeparam>
        /// <param name="cacheId">The cache id.</param>
        /// <param name="type">The type of the object.</param>
        /// <param name="getItemCallback">The function the will query the object and set the cache.</param>
        /// <param name="secondsToCache">The longevity of the object if it needs to be set</param>
        /// <param name="priority">The priority by which to cache the item if it needs to be set</param>
        /// <returns>Return the object of type T either from the cache or from the callback query. </returns>
        T Get<T>(string cacheId, CacheType type, Func<T> getItemCallback, int secondsToCache, CacheItemPriority priority) where T : class, IFlushable;

        /// <summary>
        /// Deletes item from cache.
        /// </summary>
        /// <param name="cacheId">The cache id.</param>
        /// <param name="type">The type of the object.</param>
        void Delete(string cacheId, CacheType type);
    }
}

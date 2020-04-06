using System;
using System.Collections.Generic;
using System.Text;

namespace SBMS.Mobile.Services.Caching
{
   public class CacheManagerService
    {
        //private readonly IMemoryCache _cache;

        ///// <summary>
        ///// Get a cached item. If it's not in the cache yet, then load and cache it
        ///// </summary>
        ///// <typeparam name="T">Type of cached item</typeparam>
        ///// <param name="key">Cache key</param>
        ///// <param name="acquire">Function to load item if it's not in the cache yet</param>
        ///// <param name="cacheTime">Cache time in minutes; pass 0 to do not cache; pass null to use the default time</param>
        ///// <returns>The cached value associated with the specified key</returns>
        //public virtual T Get<T>(string key, Func<T> acquire, int? cacheTime = null)
        //{
        //    //item already is in cache, so return it
        //    if (_cache.TryGetValue(key, out T value))
        //        return value;

        //    //or create it using passed function
        //    var result = acquire();

        //    //and set in cache (if cache time is defined)
        //    if ((cacheTime ?? NopCachingDefaults.CacheTime) > 0)
        //        Set(key, result, cacheTime ?? NopCachingDefaults.CacheTime);

        //    return result;
        //}

        ///// <summary>
        ///// Adds the specified key and object to the cache
        ///// </summary>
        ///// <param name="key">Key of cached item</param>
        ///// <param name="data">Value for caching</param>
        ///// <param name="cacheTime">Cache time in minutes</param>
        //public virtual void Set(string key, object data, int cacheTime)
        //{
        //    if (data != null)
        //    {
        //        _cache.Set(AddKey(key), data, GetMemoryCacheEntryOptions(TimeSpan.FromMinutes(cacheTime)));
        //    }
        //}

        ///// <summary>
        ///// Gets a value indicating whether the value associated with the specified key is cached
        ///// </summary>
        ///// <param name="key">Key of cached item</param>
        ///// <returns>True if item already is in cache; otherwise false</returns>
        //public virtual bool IsSet(string key)
        //{
        //    return _cache.TryGetValue(key, out object _);
        //}

        ///// <summary>
        ///// Perform some action with exclusive in-memory lock
        ///// </summary>
        ///// <param name="key">The key we are locking on</param>
        ///// <param name="expirationTime">The time after which the lock will automatically be expired</param>
        ///// <param name="action">Action to be performed with locking</param>
        ///// <returns>True if lock was acquired and action was performed; otherwise false</returns>
        //public bool PerformActionWithLock(string key, TimeSpan expirationTime, Action action)
        //{
        //    //ensure that lock is acquired
        //    if (!_allKeys.TryAdd(key, true))
        //        return false;

        //    try
        //    {
        //        _cache.Set(key, key, GetMemoryCacheEntryOptions(expirationTime));

        //        //perform action
        //        action();

        //        return true;
        //    }
        //    finally
        //    {
        //        //release lock even if action fails
        //        Remove(key);
        //    }
        //}

        ///// <summary>
        ///// Removes the value with the specified key from the cache
        ///// </summary>
        ///// <param name="key">Key of cached item</param>
        //public virtual void Remove(string key)
        //{
        //    _cache.Remove(RemoveKey(key));
        //}

        ///// <summary>
        ///// Removes items by key pattern
        ///// </summary>
        ///// <param name="pattern">String key pattern</param>
        //public virtual void RemoveByPattern(string pattern)
        //{
        //    //get cache keys that matches pattern
        //    var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        //    var matchesKeys = _allKeys.Where(p => p.Value).Select(p => p.Key).Where(key => regex.IsMatch(key)).ToList();

        //    //remove matching values
        //    foreach (var key in matchesKeys)
        //    {
        //        _cache.Remove(RemoveKey(key));
        //    }
        //}


    }
}

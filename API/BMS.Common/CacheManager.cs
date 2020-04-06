using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Common
{
    public static class CacheManager
    {
        public static T GetOrSet<T>(CacheKeys cacheKey, Func<T> getItemCallback) where T : class
        {
            T item = MemoryCache.Default.Get(cacheKey.ToString()) as T;
            if (item == null)
            {
                item = getItemCallback();
                MemoryCache.Default.Add(cacheKey.ToString(), item, DateTime.Now.AddMonths(1));
            }
            return item;
        }

        public static void Clear(CacheKeys cacheKey)
        {
            string existingKey = MemoryCache.Default.Where(m => m.Key == cacheKey.ToString()).Select(kvp => kvp.Key).FirstOrDefault();
            if (!string.IsNullOrEmpty(existingKey))
            {
                MemoryCache.Default.Remove(cacheKey.ToString());
            }
        }
    }
}

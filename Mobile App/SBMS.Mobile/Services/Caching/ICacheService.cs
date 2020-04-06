using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SBMS.Mobile.Services.Caching
{
    public interface ICacheService
    {
        Task<T> Get<T>(string key, Func<Task<T>> acquire, int? cacheTime = null);
        void Set(string key, object data);
        void Remove(string key);
    }
}

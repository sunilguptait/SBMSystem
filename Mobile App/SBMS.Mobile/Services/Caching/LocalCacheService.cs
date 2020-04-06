using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SBMS.Mobile.Services.Caching
{
    public class LocalCacheService : ICacheService
    {
        public async Task<T> Get<T>(string key, Func<Task<T>> acquire, int? cacheTime = null)
        {
            //item already is in cache, so return it
            var data = App.GetPropertyValue(key);
            if (!string.IsNullOrEmpty(data))
            {
                return JsonConvert.DeserializeObject<T>(data);// (T)data;
            }

            //or create it using passed function
            var result = await acquire();

            //and set in cache (if cache time is defined)
            var dataJson= JsonConvert.SerializeObject(result);
            App.SetPropertyValue(key, dataJson);

            return result;
        }
        public void Set(string key, object data)
        {
            if (data != null)
            {
                App.SetPropertyValue(key, data);
            }
        }
        public void Remove(string key)
        {
            Application.Current.Properties.Remove(key);
        }
    }
}

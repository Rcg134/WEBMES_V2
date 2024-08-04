namespace WEBMES_V2.Services
{
    using Microsoft.Extensions.Caching.Memory;
    using System;
    using System.Threading.Tasks;
    public class CacheManagerService
    {
        private readonly IMemoryCache _cache;

        public CacheManagerService(IMemoryCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }


        public async Task<T> GetAsync<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            if (_cache.TryGetValue(key, out T cachedValue))
            {
                return await Task.FromResult(cachedValue);
            }
            else
            {
                return default;
            }
        }

        public async Task<List<T>> GetListAsync<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            if (_cache.TryGetValue(key, out List<T> cachedList))
            {
                return await Task.FromResult(cachedList);
            }
            else
            {
                return null; 
            }
        }

        public async Task SetAsync<T>(string key, T value)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            if (value == null)
                throw new ArgumentNullException(nameof(value));
                
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                           .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                           .SetPriority(CacheItemPriority.NeverRemove);
                           
            await Task.Run(() => _cache.Set(key, value, cacheEntryOptions));
        }

        public async Task RemoveAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            await Task.Run(() => _cache.Remove(key));
        }

    }
}

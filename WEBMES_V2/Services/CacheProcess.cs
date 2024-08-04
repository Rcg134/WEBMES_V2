using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBMES_V2.Services
{
    public class CacheProcess
    {
        private readonly CacheManagerService _cacheManagerService;
        public CacheProcess(CacheManagerService cacheManagerService )
        {
            _cacheManagerService = cacheManagerService;
            
        }

        public async Task<IEnumerable<T>?> GetSetList<T>(string cacheKey ,IEnumerable values)
        {
            var getListOfCache = await _cacheManagerService.GetListAsync<IEnumerable<T>>(cacheKey);

            if (getListOfCache != null)
            {
               return (IEnumerable<T>)getListOfCache;
            }

            await _cacheManagerService.RemoveAsync(cacheKey);
            var setCache = _cacheManagerService.SetAsync(cacheKey , values);
            
            return null;
        }

       public async Task<List<T>> CheckCache<T>(string cacheKey)
       {
           return await _cacheManagerService.GetListAsync<T>(cacheKey);
       }
    }
}
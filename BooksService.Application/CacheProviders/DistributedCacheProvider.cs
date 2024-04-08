using BooksService.Application.Abstractions;
using BooksService.Domain.Models;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BooksService.Application.CacheProviders
{
    public class DistributedCacheProvider : ICacheProvider
    {
        private readonly IDistributedCache _distributedCache;

        public DistributedCacheProvider(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<IEnumerable<Item>> GetOrSetAsync(string key, Func<Task<IEnumerable<Item>>> valueFactory, TimeSpan expiry)
        {
            var cachedData = await _distributedCache.GetStringAsync(key);
            if (cachedData != null)
            {
                return JsonSerializer.Deserialize<IEnumerable<Item>>(cachedData);
            }

            var result = await valueFactory();
            await _distributedCache.SetStringAsync(key, JsonSerializer.Serialize(result), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiry
            });
            return result;
        }
    }
}

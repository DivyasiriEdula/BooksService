using BooksService.Application.Abstractions;
using BooksService.Domain.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksService.Application.CacheProviders
{
    public class InMemoryCacheProvider : ICacheProvider
    {
        private readonly IMemoryCache _memoryCache;

        public InMemoryCacheProvider(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<Item>> GetOrSetAsync(string key, Func<Task<IEnumerable<Item>>> valueFactory, TimeSpan expiry)
        {
            if (!_memoryCache.TryGetValue(key, out IEnumerable<Item> result))
            {
                result = await valueFactory();
                _memoryCache.Set(key, result, expiry);
            }
            return result;
        }
    }
}

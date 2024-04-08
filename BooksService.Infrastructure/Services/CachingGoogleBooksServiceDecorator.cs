using BooksService.Application.Abstractions;
using BooksService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksService.Infrastructure.Services
{
    public class CachingGoogleBooksServiceDecorator : IGoogleBooksServiceDecorator
    {
        private readonly IGoogleBooksService _realService;
        private readonly ICacheProvider _cacheProvider;

        public CachingGoogleBooksServiceDecorator(IGoogleBooksService realService, ICacheProvider cacheProvider)
        {
            _realService = realService ?? throw new ArgumentNullException(nameof(realService));
            _cacheProvider = cacheProvider ?? throw new ArgumentNullException(nameof(cacheProvider));
        }

        public async Task<IEnumerable<Item>> GetBooksAsync(string title, int? maxResults)
        {
            var cacheKey = $"{title}_{maxResults}";

            return await _cacheProvider.GetOrSetAsync(cacheKey, async () =>
            {
                return await _realService.GetBooksAsync(title, maxResults);
            }, TimeSpan.FromMinutes(10));
        }
    }
}

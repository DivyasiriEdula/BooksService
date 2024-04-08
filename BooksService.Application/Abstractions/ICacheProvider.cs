using BooksService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksService.Application.Abstractions
{
    public interface ICacheProvider
    {
        Task<IEnumerable<Item>> GetOrSetAsync(string key, Func<Task<IEnumerable<Item>>> valueFactory, TimeSpan expiry);
    }
}

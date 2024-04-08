using BooksService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksService.Application.Abstractions
{
    public interface IGoogleBooksService
    {
        Task<IEnumerable<Item>> GetBooksAsync(string title, int? maxResults);
    }
}

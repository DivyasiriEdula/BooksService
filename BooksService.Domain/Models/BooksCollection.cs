using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksService.Domain.Models
{
    public class BooksCollection
    {
        public string? kinds { get; set; }
        public int? totalItems { get; set; }
        public List<Item> items { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksService.Domain.Models
{
    public class BooksResponse
    {
        public string kind { get; set; }
        public string id { get; set; }
        public string etag { get; set; }
        public string selfLink { get; set; }
        public string Language { get; set; }
        public string title { get; set; }
        public string publisher { get; set; }
        public string publishedDate { get; set; }
        public string description { get; set; }
        public List<string> categories { get; set; }
    }
}

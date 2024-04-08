    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksService.Infrastructure.Exceptions
{
    public class BooksApiException : Exception
    {
        public BooksApiException() { }

        public BooksApiException(string message) : base(message) { }

        public BooksApiException(string message, Exception innerException) : base(message, innerException) { }
    }
}

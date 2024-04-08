using BooksService.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksService.Application.Queries
{
    public class GetBooksQuery : IRequest<IEnumerable<BooksResponse>>
    {
        public string? Title { get; set; }
        public int? MaxResults { get; set; }
    }
}

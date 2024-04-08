using AutoMapper;
using BooksService.Application.Abstractions;
using BooksService.Application.Queries;
using BooksService.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksService.Application.QueryHandlers
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IEnumerable<BooksResponse>>
    {
        private readonly IGoogleBooksServiceDecorator _googleBooksServiceDecorator;
        private readonly IMapper _mapper;

        public GetBooksQueryHandler(IGoogleBooksServiceDecorator googleBooksServiceDecorator, IMapper mapper)
        {
            _googleBooksServiceDecorator = googleBooksServiceDecorator;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BooksResponse>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var items = await _googleBooksServiceDecorator.GetBooksAsync(request.Title, request.MaxResults);
            var booksResponse = _mapper.Map<IEnumerable<BooksResponse>>(items);
            return booksResponse;
        }
    }
}

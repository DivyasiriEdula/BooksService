using BooksService.Application.Queries;
using BooksService.Domain.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;


namespace BookServiceAPP.API.Controllers
{
    [Route("api/internal/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<GetBooksQuery> _validator;
        public BooksController(IMediator mediator, IValidator<GetBooksQuery> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IEnumerable<BooksResponse>> Get(ODataQueryOptions<BooksResponse> options, [FromQuery] GetBooksQuery query)
        {
            var validationResult = await _validator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var books = await _mediator.Send(query);

            //Get the count before returning the result
            var count = books.Count();

            // Return the IQueryable<BooksResponse>
            return books.AsQueryable();
        }

    }
}

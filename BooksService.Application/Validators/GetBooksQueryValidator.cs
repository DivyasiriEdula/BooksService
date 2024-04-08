using BooksService.Application.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksService.Application.Validators
{
    public class GetBooksQueryValidator : AbstractValidator<GetBooksQuery>
    {
        public GetBooksQueryValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be empty");
            RuleFor(x => x.MaxResults).GreaterThan(0).When(x => x.MaxResults.HasValue).WithMessage("MaxResults must be greater than 0");
        }

    }
}

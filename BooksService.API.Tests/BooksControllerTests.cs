using BookServiceAPP.API.Controllers;
using BooksService.Application.Queries;
using BooksService.Domain.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksService.API.Tests
{

    [TestFixture]
    public class BooksControllerTests
    {
        private BooksController _controller;
        private IMediator _mediator;
        private IValidator<GetBooksQuery> _validator;

        [SetUp]
        public void Setup()
        {
            _mediator = Substitute.For<IMediator>();
            _validator = Substitute.For<IValidator<GetBooksQuery>>();
            _controller = new BooksController(_mediator, _validator);
        }

        [Test]
        public async Task Get_ReturnsCorrectResult()
        {
            // Arrange
            var query = new GetBooksQuery();
            var booksResponse = new List<BooksResponse> { new BooksResponse() };
            _validator.ValidateAsync(query).Returns(new FluentValidation.Results.ValidationResult());
            _mediator.Send(query).Returns(booksResponse);

            // Act
            var result = await _controller.Get(null, query);

            // Assert
            Assert.IsInstanceOf<IEnumerable<BooksResponse>>(result);
            Assert.AreEqual(booksResponse.Count, result.Count());
        }

        [Test]
        public void Get_InvalidQuery_ThrowsValidationException()
        {
            // Arrange
            var query = new GetBooksQuery();
            _validator.ValidateAsync(query).Returns(new FluentValidation.Results.ValidationResult { Errors = { new ValidationFailure("property", "error message") } });

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(async () => await _controller.Get(null, query));
        }
    }
}

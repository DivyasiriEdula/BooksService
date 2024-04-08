using AutoMapper;
using BooksService.Application.Abstractions;
using BooksService.Application.Queries;
using BooksService.Application.QueryHandlers;
using BooksService.Domain.Models;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksService.Application.Tests
{
    [TestFixture]
    public class GetBooksQueryHandlerTests
    {
        private GetBooksQueryHandler _handler;
        private IGoogleBooksServiceDecorator _googleBooksService;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _googleBooksService = Substitute.For<IGoogleBooksServiceDecorator>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetBooksQueryHandler(_googleBooksService, _mapper);
        }

        [Test]
        public async Task Handle_ReturnsCorrectBooksResponse()
        {
            // Arrange
            var query = new GetBooksQuery { Title = "Harry Potter", MaxResults = 10 };
            var googleBooks = new List<Item> { new Item() };
            var booksResponse = new List<BooksResponse> { new BooksResponse() };

            _googleBooksService.GetBooksAsync(query.Title, query.MaxResults).Returns(googleBooks);
            _mapper.Map<IEnumerable<BooksResponse>>(googleBooks).Returns(booksResponse);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(booksResponse, result);
        }

        [Test]
        public async Task Handle_ReturnsEmptyList_WhenGoogleBooksServiceReturnsNull()
        {
            // Arrange
            var query = new GetBooksQuery { Title = "Harry Potter", MaxResults = 10 };

            _googleBooksService.GetBooksAsync(query.Title, query.MaxResults).Returns((List<Item>)null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }
    }
}

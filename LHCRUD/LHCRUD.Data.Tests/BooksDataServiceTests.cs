using LHCRUD.Data.Models;
using LHCRUD.Data.Repositories;
using LHCRUD.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LHCRUD.Data.Tests
{
    public class BooksDataServiceTests
    {
        private BooksDataService _service;
        private BookRepository _repo;
        [SetUp]
        public void Setup()
        {
            var data = new List<Book>
            {
                new Book
                {
                    Id=1,
                    ISBN = "123"
                },
                new Book
                {
                    Id=2,
                    ISBN = "234"
                },
                new Book
                {
                    Id=3,
                    ISBN = "345"
                },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Book>>();
            mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(t => t.Books).Returns(mockSet.Object);
            _repo = new BookRepository(mockContext.Object);
            _service = new BooksDataService(_repo);
        }

        [Test]
        public async Task TestPartialUpdateAsyncToChangeTitle()
        {
            Book currentBook = _repo.GetBookById(1);
            string initialTitle = currentBook.Title;
            string initialISBN = currentBook.ISBN;

            Book result = await _service.PartialUpdateBookAsync(new Book()
            {
                Id = currentBook.Id,
                Title = "New Title"
            });
            Assert.AreEqual(initialISBN, result.ISBN);
            Assert.AreNotEqual(initialTitle, result.Title);
        }

        [Test]
        public async Task TestPartialUpdateAsyncToChangeDate()
        {
            Book currentBook = _repo.GetBookById(1);
            DateTime initialPublish = currentBook.Published;
            string initialISBN = currentBook.ISBN;

            Book result = await _service.PartialUpdateBookAsync(new Book()
            {
                Id = currentBook.Id,
                Published = new DateTime(2000, 10, 10)
            });
            Assert.AreEqual(initialISBN, result.ISBN);
            Assert.AreNotEqual(initialPublish, result.Published);
        }

        [Test]
        public async Task TestPartialUpdateAsyncToChangeMultipleFields()
        {
            Book currentBook = _repo.GetBookById(1);
            DateTime initialPublish = currentBook.Published;
            string initialTitle = currentBook.Title;
            string initialPublisher = currentBook.Publisher;
            string initialISBN = currentBook.ISBN;

            Book result = await _service.PartialUpdateBookAsync(new Book()
            {
                Id = currentBook.Id,
                Published = new DateTime(2000, 10, 10),
                Title = "New Title",
                Publisher = "Not EA"
            });
            Assert.AreEqual(initialISBN, result.ISBN);
            Assert.AreNotEqual(initialPublish, result.Published);
            Assert.AreNotEqual(initialTitle, result.Title);
            Assert.AreNotEqual(initialPublisher,result.Publisher);
        }
    }
}

using LHCRUD.Data.Models;
using LHCRUD.Data.Repositories;
using LHCRUD.Services;
using LHCRUD.Services.Mapping;
using LHCRUD.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHCRUD.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IBooksDataService _booksService;
        private IBookRepository _bookRepository;
        public BooksController(IBooksDataService booksService, IBookRepository bookRepository)
        {
            _booksService = booksService;
            _bookRepository = bookRepository;

        }
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(_bookRepository.GetBookById(id));
        }

        /// <summary>
        /// Gets book by ISBN.
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        [HttpGet("isbn'/{isbn}")]
        public ActionResult GetByISBN(string isbn)
        {
            return Ok(_bookRepository.GetBookByISBN(isbn));
        }

        /// <summary>
        /// Gets books by author. Works with partial author names.
        /// </summary>
        /// <param name="name">Name or part of the name of the author</param>
        /// <returns></returns>
        [HttpGet("author'/{name}")]
        public ActionResult GetByAuthor(string name)
        {
            return Ok(_bookRepository.GetBooksByAuthor(name));
        }

        /// <summary>
        /// Updates the non null properties of a book. Id is required.
        /// </summary>
        /// <param name="partialBook">The partial book model which to update.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Thrown when after the update there are two same ISBNs</exception>
        [HttpPatch]
        public async Task<ActionResult> UpdatePartial(Book partialBook)
        {
            return Ok(await _booksService.PartialUpdateBookAsync(partialBook));
        }

        /// <summary>
        /// Does a full update of the given book.
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Thrown when after the update there are two same ISBNs</exception>
        [HttpPut]
        public async Task<ActionResult> UpdateFull(Book book)
        {
            await _bookRepository.UpdateBookAsync(book);
            return Ok();
        }

        /// <summary>
        /// Creates a book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Thrown if there is another book with the same ISBN</exception>
        [HttpPost]
        public async Task<ActionResult> Create(BookInputModel book)
        {
            await _bookRepository.AddBookAsync(Mapper.ToBook(book));
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _bookRepository.DeleteBookAsync(id);
            return Ok();
        }
    }
}

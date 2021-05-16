using LHCRUD.Data.Models;
using LHCRUD.Data.Repositories;
using System;
using System.Threading.Tasks;

namespace LHCRUD.Services
{
    public class BooksDataService
    {
        private IBookRepository _bookRepository;
        public BooksDataService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        /// <summary>
        /// This method does a partial update by checking for values in each property. If the update model doesn't have a value for a property the method doesn't update it.
        /// </summary>
        /// <param name="partialBook">Partial model which to use for the update. Id is required</param>
        /// <returns>The updated entry</returns>
        /// <exception cref="ArgumentException">Thrown when after the update there are two same ISBNs</exception>
        public async Task<Book> PartialUpdateBookAsync(Book partialBook)
        {
            var toUpdate =  _bookRepository.GetBookById(partialBook.Id);
            if (!string.IsNullOrEmpty(partialBook.Title))
            {
                toUpdate.Title = partialBook.Title;
            }
            if (!string.IsNullOrEmpty(partialBook.Author))
            {
                toUpdate.Author = partialBook.Author;
            }
            if (!string.IsNullOrEmpty(partialBook.Genre))
            {
                toUpdate.Genre = partialBook.Genre;
            }
            if (!string.IsNullOrEmpty(partialBook.Description))
            {
                toUpdate.Description = partialBook.Description;
            }
            if (!string.IsNullOrEmpty(partialBook.ISBN))
            {
                toUpdate.ISBN = partialBook.ISBN;
            }
            if (!string.IsNullOrEmpty(partialBook.Image))
            {
                toUpdate.Image = partialBook.Image;
            }
            if (partialBook.Published != null)
            {
                toUpdate.Published = partialBook.Published;
            }
            if (!string.IsNullOrEmpty(partialBook.Publisher))
            {
                toUpdate.Publisher = partialBook.Publisher;
            }
            await _bookRepository.UpdateBookAsync(toUpdate);
            return toUpdate;
        }

    }
}

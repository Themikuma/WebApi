using LHCRUD.Data.Models;
using LHCRUD.Data.Repositories;
using System;
using System.Threading.Tasks;

namespace LHCRUD.Services
{
    public class BooksDataService : IBooksDataService
    {
        private IBookRepository _bookRepository;
        public BooksDataService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<Book> PartialUpdateBookAsync(Book partialBook)
        {
            var toUpdate = _bookRepository.GetBookById(partialBook.Id);
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

using LHCRUD.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LHCRUD.Data.Repositories
{
    public interface IBookRepository
    {
        /// <summary>
        /// Adds a book to the library.
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Thrown if there is another book with the same ISBN</exception>
        Task AddBookAsync(Book book);
        Task DeleteBookAsync(int id);
        Book GetBookById(int id);
        Book GetBookByISBN(string isbn);
        /// <summary>
        /// Gets all books by an author. Works with partial author names.
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        IEnumerable<Book> GetBooksByAuthor(string author);
        IEnumerable<Book> GetBooksPublishedAfterDate(DateTime start);

        /// <summary>
        /// This method does a full update and replaces all values with the given object.
        /// </summary>
        /// <param name="updatedBook"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Thrown when after the update there are two same ISBNs</exception>
        Task UpdateBookAsync(Book updatedBook);
    }
}
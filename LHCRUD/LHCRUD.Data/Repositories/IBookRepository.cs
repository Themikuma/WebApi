using LHCRUD.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LHCRUD.Data.Repositories
{
    public interface IBookRepository
    {
        Task AddBookAsync(Book book);
        Task DeleteBook(int id);
        Book GetBookById(int id);
        Book GetBookByISBN(string isbn);
        IEnumerable<Book> GetBooksByAuthor(string author);
        IEnumerable<Book> GetBooksPublishedAfterDate(DateTime start);
        Task UpdateBookAsync(Book updatedBook);
    }
}
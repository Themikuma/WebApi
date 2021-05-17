using LHCRUD.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LHCRUD.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private const string DuplicateMessage = "Duplicate ISBNs. Make sure the book ISBN is unique";
        public BookRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddBookAsync(Book book)
        {
            if (_dbContext.Books.Any(t => t.ISBN.Equals(book.ISBN)))
            {
                throw new ArgumentException(DuplicateMessage);
            }
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();
        }

        public Book GetBookById(int id)
        {
            return _dbContext.Books.Single(t => t.Id.Equals(id));
        }

        public Book GetBookByISBN(string isbn)
        {
            return _dbContext.Books.Single(t => t.ISBN.Equals(isbn));
        }

        public IEnumerable<Book> GetBooksByAuthor(string author)
        {
            return _dbContext.Books.Where(t => t.Author.Contains(author));
        }

        public IEnumerable<Book> GetBooksPublishedAfterDate(DateTime start)
        {
            return _dbContext.Books.Where(t => t.Published > start);
        }

        public async Task UpdateBookAsync(Book updatedBook)
        {
            try
            {
                _dbContext.Books.Update(updatedBook);
                //CheckISBN(updatedBook);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new ArgumentNullException($"No book with id:{updatedBook.Id} found. Aborting update");
            }
            catch (DbUpdateException)
            {
                throw new ArgumentException(DuplicateMessage);
            }
        }

        public async Task DeleteBookAsync(int id)
        {
            var toRemove = await _dbContext.Books.FindAsync(id);
            _dbContext.Books.Remove(toRemove);
            await _dbContext.SaveChangesAsync();
        }
    }
}

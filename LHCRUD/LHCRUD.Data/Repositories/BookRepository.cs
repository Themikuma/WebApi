using LHCRUD.Data.Models;
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
            if (_dbContext.Books.Any(t => t.ISBN.Equals(book.ISBN, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException(DuplicateMessage);
            }
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();
        }

        public Book GetBookById(int id)
        {
            return  _dbContext.Books.Single(t => t.Id.Equals(id));
        }

        public Book GetBookByISBN(string isbn)
        {
            return _dbContext.Books.Single(t => t.ISBN.Equals(isbn, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Book> GetBooksByAuthor(string author)
        {
            return _dbContext.Books.Where(t => t.Author.Equals(author, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Book> GetBooksPublishedAfterDate(DateTime start)
        {
            return _dbContext.Books.Where(t => t.Published > start);
        }

        /// <summary>
        /// This method does a full update and replaces all values with the given object.
        /// </summary>
        /// <param name="updatedBook"></param>
        /// <returns></returns>
        public async Task UpdateBookAsync(Book updatedBook)
        {
            _dbContext.Books.Update(updatedBook);
            CheckISBN(updatedBook);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteBook(int id)
        {
            var toRemove = await _dbContext.Books.FindAsync(id);
            _dbContext.Books.Remove(toRemove);
            await _dbContext.SaveChangesAsync();
        }

        // Make sure there is no more than one entry that has this ISBN. 
        private void CheckISBN(Book book)
        {
            try
            {
                _dbContext.Books.SingleOrDefault(t => t.ISBN.Equals(book.ISBN));

            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException(DuplicateMessage);
            }
        }
    }
}

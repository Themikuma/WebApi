using LHCRUD.Data.Models;
using System.Threading.Tasks;

namespace LHCRUD.Services
{
    public interface IBooksDataService
    {
        /// <summary>
        /// This method does a partial update by checking for values in each property. If the update model doesn't have a value for a property the method doesn't update it.
        /// </summary>
        /// <param name="partialBook">Partial model which to use for the update. Id is required</param>
        /// <returns>The updated entry</returns>
        /// <exception cref="ArgumentException">Thrown when after the update there are two same ISBNs</exception>
        Task<Book> PartialUpdateBookAsync(Book partialBook);
    }
}
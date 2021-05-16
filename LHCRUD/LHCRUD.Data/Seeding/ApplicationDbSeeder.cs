using LHCRUD.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LHCRUD.Data.Seeding
{
    public class ApplicationDbSeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            if (dbContext.Books.Any())
            {
                return;
            }
            var books = await DataRetrieval.GetBooks(100);
            await dbContext.AddRangeAsync(books);
            await dbContext.SaveChangesAsync();
        }
    }
}

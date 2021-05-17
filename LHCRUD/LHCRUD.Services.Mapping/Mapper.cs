using LHCRUD.Data.Models;
using LHCRUD.Web.ViewModels;
using System;

namespace LHCRUD.Services.Mapping
{
    public static class Mapper
    {
        public static Book ToBook(BookInputModel book)
        {
            return new Book()
            {
                Author = book.Author,
                Description = book.Description,
                Genre = book.Genre,
                Image = book.Image,
                ISBN = book.ISBN,
                Published = book.Published,
                Publisher = book.Publisher,
                Title = book.Title
            };
        }
    }
}

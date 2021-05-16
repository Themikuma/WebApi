using System;

namespace LHCRUD.Data.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public string Image { get; set; }
        public DateTime Published { get; set; }
        public string Publisher { get; set; }
    }
}

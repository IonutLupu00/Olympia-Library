using Olympia_Library.Data;

namespace Olympia_Library.Data
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        
        public int AuthorId { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}

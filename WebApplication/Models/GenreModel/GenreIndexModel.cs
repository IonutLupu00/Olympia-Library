using Olympia_Library.Models.BookModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Olympia_Library.Models.GenreModel
{
    public class GenreIndexModel
    {
        public string GenreName { get; set; }
        public IEnumerable<BookListingModel> BookList { get; set; } 
    }
}

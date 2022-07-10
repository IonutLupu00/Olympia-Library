using Olympia_Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Olympia_Library.Models.BookModel
{
    public class BookIndexModel
    {
        public IEnumerable<BookListingModel> BookListing { get; set; }
        public IEnumerable<Genre> GenreList { get; set; }

    }
}

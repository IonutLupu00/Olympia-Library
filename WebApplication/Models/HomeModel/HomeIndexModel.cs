using Olympia_Library.Data;
using Olympia_Library.Models.BookModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Olympia_Library.Models.HomeModel
{
    public class HomeIndexModel
    {
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<BookListingModel> BookListing { get; set; }
        public IEnumerable<BookListingModel> LatestAdditions { get; set; }
        public string SearchQuery { get; set; }
    }
}

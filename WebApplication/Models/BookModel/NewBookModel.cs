using Microsoft.AspNetCore.Http;
using Olympia_Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{

    public class NewBookModel
    {
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile CoverImage { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}

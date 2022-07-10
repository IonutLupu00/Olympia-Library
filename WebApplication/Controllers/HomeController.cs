using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Olympia_Library.Models.BookModel;
using Olympia_Library.Models.HomeModel;
using Olympia_Library.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Repositories;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookService _bookService;
        private readonly HomeService _homeService;
        private readonly AuthorService _authorService;

        public HomeController(ILogger<HomeController> logger, BookService bookService, HomeService homeService, AuthorService authorService)
        {
            _logger = logger;
            _bookService = bookService;
            _homeService = homeService;
            _authorService = authorService;
        }
        public IActionResult Index()
        {
            return View(_homeService.BuildHomeIndexModel());
        }
        [HttpPost]
        public IActionResult Index(string autocompleteString)
        {
            ViewBag.Message = autocompleteString;
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminActions()
        {
            return View();
        }


        public IActionResult BookSpecial()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Search(HomeIndexModel model)
        {
            var searchString = model.SearchQuery.ToLower();

            var allBooks = _bookService.GetAll().Where(
            book => book.Title.ToLower().Contains(searchString) 
            || _authorService.GetAuthorByCondition(a => a.AuthorId == book.AuthorId).FirstOrDefault().Name.ToLower().Contains(searchString)           
            );

            var bookListing = allBooks.Select(book => new BookListingModel { 
                Title = book.Title,
                BookId = book.BookId,
                AuthorId = book.AuthorId,
                GenreId = book.GenreId,
                ImageUrl = book.ImageUrl,
                Description = book.Description,
                Price = book.Price
            });

            model.BookListing = bookListing;

            return View(model);
        }


    }
}

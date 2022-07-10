using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Olympia_Library.Data;
using Olympia_Library.Models.BookModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using WebApplication.Models;
using WebApplication.Repositories;
using WebApplication.Services;

namespace WebApplication.Controllers
{

    public class BookController : Controller
    {
        private readonly BookService _bookService;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly AuthorService _authorService;
        private readonly GenreService _genreService;
        public BookController(BookService bookService, IRepositoryWrapper repositoryWrapper, AuthorService authorService,GenreService genreService)
        {
            _bookService = bookService;
            _repositoryWrapper = repositoryWrapper;
            _authorService = authorService;
            _genreService = genreService;
        }

        public IActionResult Index()
        {
            var books = _bookService.GetAll();

            var bookListing = books.Select(b => new BookListingModel
            {
                Title = b.Title,
                AuthorId = b.AuthorId,
                ImageUrl = b.ImageUrl,
                GenreId = b.GenreId,
                BookId = b.BookId,
                Description = b.Description,
                Price = b.Price
            });

            
            var genres = _repositoryWrapper.GenreRepository.FindByCondition(g => g.Name != null);
       

            var model = new BookIndexModel
            {
                BookListing = bookListing,
                GenreList = genres
            };

            return View(model);
        }

       

        public IActionResult Detail(int id)
        {
                return View(_bookService.BuildBookDetailModel(id));    
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddBook()
        {
            return View(new BookModel());
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddBook(BookModel book)
        {
            try
            {

                _bookService.AddBook(book);
                _bookService.Save();
                ModelState.Clear();
                ViewData["Message"] = "1";
            }       
                          
            catch
            {
                ViewData["Message"] = "0";
            }

            return View();

        }

        [Authorize(Roles = "Admin")]
        public IActionResult RemoveBook()
        {
            return View();
        }
        

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult RemoveBook(BookModel book)
        {

            try
            {
                _bookService.DeleteBook(book);
                _bookService.Save();
                ModelState.Clear();
                ViewData["Message"] = "1";
            }
            catch
            {
                ViewData["Message"] = "0";
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult EditBook()
        {
            return View();
        }
        

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult EditBook(BookModel book)
        {
            try
            {
                _bookService.UpdateBook(book);
                _bookService.Save();
                ModelState.Clear();
                ViewData["Message"] = "1";
            }
            catch
            {
                ViewData["Message"] = "0";
            }
            return View();
        }

        [AllowAnonymous]
        public IActionResult RandomBook()
        {

            var randomBookId = _bookService.GetRandomBook().BookId;
            
            return RedirectToAction("Detail", "Book", new {id = randomBookId });
        }

        //[AllowAnonymous]
        //public IActionResult BookOfTheDay()
        //{

        //}

        
        public ActionResult GetAuthors()
        {
            return Json(_authorService.ExtractAuthorNames());

        }

        
        public ActionResult GetTitles()
        {
            return Json(_bookService.ExtractBookTitles());
        }

        
        public ActionResult GetGenres()
        {
            return Json(_genreService.ExtractGenres());
        }



    }
}

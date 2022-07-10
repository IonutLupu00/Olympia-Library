using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Olympia_Library.Data;
using Olympia_Library.Models.BookModel;
using Olympia_Library.Models.GenreModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Repositories;
using WebApplication.Services;

namespace Olympia_Library.Controllers
{
    public class GenreController : Controller
    {
        private readonly BookService _bookService;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly GenreService _genreService;

        public GenreController(BookService bookService, IRepositoryWrapper repositoryWrapper, GenreService genreService)
        {
            _bookService = bookService;
            _repositoryWrapper = repositoryWrapper;
            _genreService = genreService;
        }

        public IActionResult Index(int id)
        {
            var books = _bookService.GetBooksByCondition(b => b.GenreId == id);

            var bookList = books.Select(b => new BookListingModel {
                BookId = b.BookId,
                ImageUrl = b.ImageUrl
            });
            var model = new GenreIndexModel {
                BookList = bookList,
                GenreName = _repositoryWrapper.GenreRepository.FindByCondition(g => g.Id == id).FirstOrDefault().Name
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddGenre()
        {
            return View(new NewGenreModel { });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddGenre(NewGenreModel model)
        {
            try
            {
                _genreService.AddGenre(model);

                _genreService.Save();
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
        public IActionResult EditGenre()
        {
            return View(new NewGenreModel { });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult EditGenre(NewGenreModel model)
        {
            if(model.Name == null)
            {
                return View();
            }


            try
            {

                _genreService.UpdateGenre(model);
                _genreService.Save();
                ModelState.Clear();
                ViewData["Message"] = "1";
            }

            catch
            {
                ViewData["Message"] = "0";
            }
            

            return View();
        }
    }
}

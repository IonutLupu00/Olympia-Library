using Microsoft.AspNetCore.Mvc;
using WebApplication.Services;
using WebApplication.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminAuthorController : Controller
    {
        private readonly AuthorService _authorService;

        public AdminAuthorController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        public IActionResult AddAuthor()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddAuthor(AuthorModel Author)
        {

            try
            {
                _authorService.AddAuthor(Author);
                _authorService.Save();
                ModelState.Clear();
                ViewData["Message"] = "1";
            }
            catch
            {
                ViewData["Message"] = "0";
            }
            return View();

        }

        public IActionResult RemoveAuthor()
        {
            return View();
        }
        

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult RemoveAuthor(AuthorModel Author)
        {
            try
            {
                 _authorService.DeleteAuthor(Author);
                _authorService.Save();
                ModelState.Clear();
                ViewData["Message"] = "1";
            }
            catch
            {
                ViewData["Message"] = "0";
            }
            return View();

        }


        public IActionResult EditAuthor()
        {
            var model = new AuthorModel { };
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]

        public ActionResult EditAuthor(AuthorModel Author)
        {
            try
            {
                _authorService.UpdateAuthor(Author);
                _authorService.Save();
                ModelState.Clear();
                ViewData["Message"] ="1";
            }
            catch
            {
                ViewData["Message"] ="0";
            }
            return View();
        }


        public ActionResult GetAuthors()
        {
            return Json(_authorService.ExtractAuthorNames());

        }

    }


}

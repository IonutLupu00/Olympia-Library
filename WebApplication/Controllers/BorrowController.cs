using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Olympia_Library.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using WebApplication.Models;
using WebApplication.Services;

namespace Olympia_Library.Controllers
{

    public class BorrowController : Controller
    {
        private readonly BranchService _branchService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly BorrowService _borrowService;
        public BorrowController(BranchService branchService,BorrowService borrowService, UserManager<ApplicationUser> userManager)
        {
            _branchService = branchService;
            _userManager = userManager;
            _borrowService = borrowService;
        }

        [Authorize]
        public IActionResult Borrow(string title)
        {

            return View(_branchService.GetBranchesModelContainingBook(title));
        }

        [Authorize]
        public IActionResult BorrowSpecific(string branchname,string booktitle)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Uri uri = new Uri(booktitle); 
            string title = HttpUtility.ParseQueryString(uri.Query).Get("title");

            try
            {
                _borrowService.borrow(userId, title, branchname);
                _borrowService.Save();
                ViewData["Message"] = "1";
            }
            catch
            {
                ViewData["Message"] = "0";
            }
            return View();
        }

        
        public IActionResult GetBorrowedBooks() {
            return Json(_borrowService.GetBorrowsByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)).borrows);

        }

    }
}

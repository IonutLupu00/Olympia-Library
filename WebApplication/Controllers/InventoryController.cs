using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class InventoryController : Controller
    {
        private readonly StockService _stockService;
        private readonly BookService _bookService;
        public InventoryController(StockService stockService,BookService bookService)
        {
            _stockService = stockService;
            _bookService = bookService;
        }

        public ActionResult SpecificInventory(string branch)
        {
            return View(_stockService.GetStockModelByBranchName(branch));
        }



        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditInventory(StockModel stock)
        {
            try
            {
                _stockService.UpdateInventory(stock);
                _stockService.Save();
                ViewData["Message"] = "1";
            }
            catch {
                ViewData["Message"] = "0";
            }
            return View("SpecificInventory", _stockService.GetStockModelByBranchName(stock.LibraryName));
        }

        public ActionResult GetTitles()
        {
            return Json(_bookService.ExtractBookTitles());
        }


    }
}

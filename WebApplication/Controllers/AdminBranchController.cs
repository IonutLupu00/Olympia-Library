using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Olympia_Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBranchController : Controller
    {
        private readonly BranchService _branchService;
        private readonly StockService _stockService;



        public AdminBranchController(BranchService BranchService, StockService StockService)
        {
            _branchService = BranchService;
            _stockService = StockService;

        }

        public ActionResult AddBranch()
        {
            return View(new BranchModel { });
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddBranch(BranchModel Branch)
        {

            try
            {
                _branchService.AddBranch(Branch);
                _branchService.Save();
                ModelState.Clear();
                ViewData["Message"] = "1";
            }
            catch
            {
                ViewData["Message"] = "0";
            }
            return View();

        }


        public ActionResult RemoveBranch()
        {
            return View(new BranchModel { });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult RemoveBranch(BranchModel Branch)
        {
            try
            {
                _branchService.DeleteBranch(Branch);
                _branchService.Save();
                ModelState.Clear();
                ViewData["Message"] = "1";
            }
            catch
            {
                ViewData["Message"] = "0";
            }
            return View();

        }

        public ActionResult EditBranch()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]

        public ActionResult EditBranch(BranchModel Branch)
        {
            try
            {
                _branchService.UpdateBranch(Branch);
                _branchService.Save();
                ModelState.Clear();
                ViewData["Message"] = "1";
            }
            catch
            {
                ViewData["Message"] = "0";
            }
            return View();
        }

        public ActionResult GetBranchNames()
        {
            return Json(_branchService.ExtractBranchNames());
        }


        public ActionResult Inventory()
        {
            List<BranchModel> branches = new List<BranchModel>();
            List<Branch> branches_raw = _branchService.GetBranchByCondition(b => b.BranchId != -1);
            foreach (var branch in branches_raw)
            {
                BranchModel br = new BranchModel(branch.Name, branch.Location);
                branches.Add(br);

                ViewData[branch.Name] = _stockService.GetStockModelByBranchId(branch.BranchId).countBooks();

            }
            return View(branches);
        }



    }
}

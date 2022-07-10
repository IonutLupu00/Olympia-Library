using Olympia_Library.Data;
using Olympia_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public class BorrowService : BaseService
    {
        private readonly BookService _bookService;
        private readonly BranchService _branchService;
        public BorrowService(IRepositoryWrapper repositoryWrapper,BookService bookService, BranchService branchService) : base(repositoryWrapper) {
            _bookService = bookService;
            _branchService = branchService;
        }

        public void borrow(string userId, string title, string branchname)
        {
            Borrow borrow = new Borrow();
            borrow.Amount = 1;
            borrow.BookId = _bookService.GetBooksByCondition(b => b.Title == title).First().BookId;
            borrow.BranchId = _branchService.GetBranchByCondition(b => b.Name == branchname).First().BranchId;
            borrow.UserId = userId;

            repositoryWrapper.BorrowRepository.Create(borrow);
        }

        public BorrowModel GetBorrowsByUserId(string id)
        {
            BorrowModel borrows = new BorrowModel();
            List<Borrow> raw_borrows = repositoryWrapper.BorrowRepository.FindByCondition(b=>b.UserId == id).ToList();

            foreach (Borrow raw_borrow in raw_borrows) {
                BorrowModel.pair borrow = new BorrowModel.pair();
                borrow.Title = _bookService.GetBooksByCondition(b => b.BookId == raw_borrow.BookId).First().Title;
                borrow.Location = _branchService.GetBranchByCondition(b => b.BranchId == raw_borrow.BranchId).First().Location;
                borrow.Library_Name = _branchService.GetBranchByCondition(b => b.BranchId == raw_borrow.BranchId).First().Name;
                borrows.borrows.Add(borrow);
            }
            return borrows;
        }

    }

    



}
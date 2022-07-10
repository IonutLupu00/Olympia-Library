using Olympia_Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public class BranchService : BaseService
    {
        private readonly BookService _bookService;
        public BranchService(IRepositoryWrapper repositoryWrapper,BookService bookService) : base(repositoryWrapper) {
            _bookService = bookService;
        }
        


        public void AddBranch(BranchModel branch)
        {
            Branch new_branch = new Branch();
            new_branch.Name = branch.Name;
            new_branch.LibraryId = 1; //----------------- TO DO
            new_branch.Location = branch.Location;

            repositoryWrapper.BranchRepository.Create(new_branch);
        }

        public List<BranchModel> GetBranchesModelContainingBook(string title)
        {
            Book book = _bookService.GetBooksByCondition(b => b.Title == title).First();
            List<Stock> stocks = repositoryWrapper.StockRepository.FindByCondition(b => b.BookId == book.BookId).ToList();
            List<int> branches_id = new List<int>();
            foreach(Stock stock in stocks)
            {
                if(stock.Quantity>0)
                branches_id.Add(stock.BranchId);
            }
            branches_id = branches_id.Distinct().ToList();

            List<BranchModel> branches = new List<BranchModel>();
            foreach(int id in branches_id)
            {
                Branch branch = GetBranchByCondition(b => b.BranchId == id).First();
                BranchModel branch_model = new BranchModel();
                branch_model.Name = branch.Name;
                branch_model.Location = branch.Location;
                branches.Add(branch_model);
            }
            return branches;            
        }

        public void UpdateBranch(BranchModel branch)
        {

            Branch updated_branch = new Branch();
            updated_branch = GetBranchByCondition(b => b.Name == branch.Name).First();
            if (branch.Location != null)
            {
                updated_branch.Location = branch.Location;
            }

            repositoryWrapper.BranchRepository.Update(updated_branch);
        }

        public void DeleteBranch(BranchModel branch)
        {
            repositoryWrapper.BranchRepository.Delete(GetBranchByCondition(b => b.Name == branch.Name).First());
        }

        public List<Branch> GetBranchByCondition(Expression<Func<Branch, bool>> expression)
        {
            return repositoryWrapper.BranchRepository.FindByCondition(expression).ToList();
        }

        public List<string> ExtractBranchNames()
        {
            List<Branch> branches = GetBranchByCondition(b => b.BranchId != -1);
            List<string> BranchNames = new List<string>();
            foreach (var author in branches)
            {
                BranchNames.Add(author.Name);
            }
            return BranchNames;
        }
    }

}
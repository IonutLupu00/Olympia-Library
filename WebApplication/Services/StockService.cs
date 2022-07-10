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
    public class StockService : BaseService
    {

        private readonly BookService _bookService;
        private readonly BranchService _branchService;
        public StockService(IRepositoryWrapper repositoryWrapper, BookService BookService, BranchService BranchService) : base(repositoryWrapper)
        {
            _bookService = BookService;
            _branchService = BranchService;

        }

        public StockModel GetStockModelByBranchId(int branchId)
        {
            StockModel stock = new StockModel();
            List<Stock> stock_raw = GetStockByCondition(b => b.BranchId == branchId);

            stock.LibraryName = _branchService.GetBranchByCondition(b => b.BranchId == branchId).FirstOrDefault().Name;

            foreach (Stock br_stock in stock_raw)
            {
                try //do not remove the try/catch statement, error trown for no apparent reason
                {
                    string title = _bookService.GetBooksByCondition(b => b.BookId == br_stock.BookId).FirstOrDefault().Title;
                    int quantity = br_stock.Quantity;
                    stock.Stock.Add(new StockModel.pair(title, quantity));
                }
                catch
                {
                    return stock;
               }
            }
            return stock;
        }

        public void UpdateInventory(StockModel stock)
        {
            Stock new_stock = new Stock();
            Stock old_stock;

            new_stock.BranchId = (_branchService.GetBranchByCondition(b => b.Name == stock.LibraryName).FirstOrDefault()).BranchId;
            new_stock.BookId = _bookService.GetBooksByCondition(b => b.Title == stock.updatedInventoryTitle).FirstOrDefault().BookId;


            try
            { //searching old stock
                old_stock = GetStockByCondition(b => (b.BranchId == new_stock.BranchId && b.BookId == new_stock.BookId)).First();
                new_stock = old_stock;
                new_stock.Quantity += stock.updatedInventoryQuantity;
            }
            catch
            {
                //old stock not found, error raised
                new_stock.Quantity = stock.updatedInventoryQuantity;
            }


            if (new_stock.Quantity < 0)
            {
                new_stock.Quantity -= stock.updatedInventoryQuantity;
                throw new InvalidOperationException("Books cannot have a negative number in stock");
            }
            try
            {
                repositoryWrapper.StockRepository.Update(new_stock);
            }
            catch
            {
                repositoryWrapper.StockRepository.Create(new_stock);

            }

        }

        public StockModel GetStockModelByBranchName(string branch)
        {
            return GetStockModelByBranchId(_branchService.GetBranchByCondition(b => b.Name == branch).FirstOrDefault().BranchId);

        }

        public List<Stock> GetStockByCondition(Expression<Func<Stock, bool>> expression)
        {
            return repositoryWrapper.StockRepository.FindByCondition(expression).ToList();
        }

    }

}
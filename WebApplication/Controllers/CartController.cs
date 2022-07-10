using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Olympia_Library.Data;
using Olympia_Library.Models.BookModel;
using Olympia_Library.Models.ShoppingModel;
using Olympia_Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Repositories;
using WebApplication.Services;

namespace Olympia_Library.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        private readonly BookService _bookService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepositoryWrapper _repositoryWrapper;
        public CartController(BookService bookService, UserManager<ApplicationUser> userManager, IRepositoryWrapper repositoryWrapper)
        {
            _bookService = bookService;
            _userManager = userManager;
            _repositoryWrapper = repositoryWrapper;
        }
     
        [Route("index")]
        public ActionResult Index()
        {

            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            if(cart != null)
            {
                ViewBag.total = cart.Sum(item => item.Book.Price * item.Quantity);
            }
            else
            {
                ViewBag.total = 0;
            }
            
            return View();
        }
        [Route("buy/{id}")]
        public IActionResult Buy(int id)
        {
            Book productModel = new Book();
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item { Book = _bookService.GetBooksByCondition(b => b.BookId == id).First(), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item { Book = _bookService.GetBooksByCondition(b => b.BookId == id).First(), Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }
        // GET: CartController/Details/5
        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        private int isExist(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Book.BookId.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }


        //------------------------------
        [Authorize]
        [Route("checkout")]
        public async Task<IActionResult>  Checkout()
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");

            //Create new transaction and add each purchase item in the database with a link to the newly created transaction
            var newTransaction = new Transaction()
            {
                Client = await _userManager.FindByEmailAsync(User.Identity.Name),
                TransactionDate = DateTime.Now
            };

            _repositoryWrapper.TransactionRepository.Create(newTransaction);
            _repositoryWrapper.Save();
            ModelState.Clear();

            foreach (var cartItem in cart)
            {
                var newPurchase = new Purchase()
                {
                    BookId = cartItem.Book.BookId,
                    Quantity = cartItem.Quantity,
                    Transaction = newTransaction                    
                };

                _repositoryWrapper.PurchaseRepository.Create(newPurchase);
                _repositoryWrapper.Save();
                ModelState.Clear();
            }

            //Clearing and returning the empty cart after the purchase is complete
            cart.Clear();
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        [Route("transactionhistory")]
        public IActionResult TransactionHistory()
        {
            var model = new TransactionIndexModel
            {
                Transactions = new List<Tuple<Transaction, int, int>>()
            };

            var userId = _userManager.GetUserId(User);

            var allPurchases = _repositoryWrapper.PurchaseRepository.FindByCondition(p => p.PurchaseId != -1).ToList();
            var allTransactions = _repositoryWrapper.TransactionRepository.FindByCondition(t => t.TransactionId != -1).Where(t=>t.Client.Id == userId).ToList();
            var allBooks = _repositoryWrapper.BookRepository.FindByCondition(b => b.BookId != -1).ToList();

            foreach(var transaction in allTransactions)
            {
                int totalSpent = 0;
                int numberOfItems = 0;

                foreach (var purchase in allPurchases)
                {
                    if(purchase.Transaction == transaction) {
                        totalSpent += purchase.Quantity + allBooks.Find(b => b.BookId == purchase.BookId).Price;
                        numberOfItems += purchase.Quantity;
                    }
                }
                if(transaction != null && totalSpent !=0)
                {
                    model.Transactions.Add(Tuple.Create(transaction,numberOfItems,totalSpent));
                }
                
            }
            return View(model);
        }
        [Authorize]
        [Route("purchaseindex")]
        public IActionResult PurchaseIndex(int id)
        {
            var purchases = _repositoryWrapper.PurchaseRepository.FindByCondition(p => p.Transaction.TransactionId == id).ToList();

            var model = new PurchaseIndexModel()
            {
                Purchases = new List<Tuple<Purchase, Book, int>>()
            };
            
            foreach(var purchase in purchases)
            {
                var book = _repositoryWrapper.BookRepository.FindByCondition(b => b.BookId == purchase.BookId).FirstOrDefault();
                model.Purchases.Add(Tuple.Create(purchase,book, book.Price * purchase.Quantity));
            }

            return View(model);
        }
    }
}

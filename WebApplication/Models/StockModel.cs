using System.Collections.Generic;

namespace WebApplication.Models
{
    
    public class StockModel
    {
        public class pair
        {
            public string bookTitle = null;
            public int bookQuantity = 0;
            
            public pair() {; }
            public pair(string title, int quantity)
            {
                bookTitle = title;
                bookQuantity = quantity;
            }
        }

        public string LibraryName { get; set; }
        public string updatedInventoryTitle{get;set;}
        public int updatedInventoryQuantity { get; set; }
        
        public List<pair> Stock = new List<pair>();

        public int countBooks() {
            int no_books = 0;
            foreach (pair pair in Stock) {
                no_books += pair.bookQuantity;
            }
            return no_books;        
        }
    }
}
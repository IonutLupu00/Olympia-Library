using Olympia_Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Olympia_Library.Models.ShoppingModel
{
    public class PurchaseIndexModel
    {
        //purchase, book, subtotal
        public List<Tuple<Purchase, Book, int>> Purchases{get;set;}
    }
}

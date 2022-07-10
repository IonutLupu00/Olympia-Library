using Olympia_Library.Data;
using Olympia_Library.Models.BookModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace Olympia_Library.Models.ShoppingModel
{
    public class Item
    {
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}

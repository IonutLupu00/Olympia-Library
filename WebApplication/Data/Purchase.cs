using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Olympia_Library.Data
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public Transaction Transaction { get; set; }
    }
}

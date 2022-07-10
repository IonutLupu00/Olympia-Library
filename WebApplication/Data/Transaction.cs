using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Olympia_Library.Data
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public ApplicationUser Client { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}

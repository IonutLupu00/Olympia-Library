using Olympia_Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Olympia_Library.Models.ShoppingModel
{
    public class TransactionIndexModel
    {   //Actual transaction, number of items, total spent
        public List<Tuple<Transaction, int, int>> Transactions { get; set; }

    }
}

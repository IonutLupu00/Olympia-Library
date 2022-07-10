using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Olympia_Library.Data
{
    public class Borrow
    {
        public int BorrowId { get;set; }
        public string UserId { get; set; }
        public int BranchId { get; set; }
        public int BookId { get; set; }
        public int Amount { get; set; }
    }
}

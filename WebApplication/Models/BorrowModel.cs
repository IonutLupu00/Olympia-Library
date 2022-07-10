using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class BorrowModel
    {
        public BorrowModel() {; }
        public string UserEmail { get; set; }
        public class pair
        {
            public pair() {; }
            public string Title { get; set; }
            public string Location { get; set; }
            public string Library_Name { get; set; }
        }
        public List<pair> borrows = new List<pair>();
    }
}

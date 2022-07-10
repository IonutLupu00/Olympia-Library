using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class BranchModel
    {
        public BranchModel() {; }

        public BranchModel(string name, string location) {
            Name = name;
            Location = location;
        }

        public string Name { get; set; }
        public string Location { get; set; }

        public StockModel Stock {get;set;}

    }
}

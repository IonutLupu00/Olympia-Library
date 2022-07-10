using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Olympia_Library.Data
{
    public class Branch
    {
        public int BranchId { get; set; }

        public string Name { get; set; }
        public int LibraryId { get; set; }
        public string Location { get; set; }
    }
}

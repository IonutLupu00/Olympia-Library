using Olympia_Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebApplication.Repositories.Abstractions;

namespace WebApplication.Repositories
{
    public class StockRepository : RepositoryBase<Stock>, IStockRepository
    {
        public StockRepository(ApplicationDbContext db) : base(db)
        {
        }

    }
}

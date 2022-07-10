using Olympia_Library.Data;
using Olympia_Library.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Repositories;

namespace Olympia_Library.Repositories.Reps
{
    public class PurchaseRepository : RepositoryBase<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}

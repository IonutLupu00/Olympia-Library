using Olympia_Library.Data;
using Olympia_Library.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Repositories;

namespace Olympia_Library.Repositories.Reps
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}

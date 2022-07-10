using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Olympia_Library.Data;

namespace WebApplication.Repositories
{
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}

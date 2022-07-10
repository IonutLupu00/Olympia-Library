using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Olympia_Library.Data;

namespace WebApplication.Repositories
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext db) : base(db)
        {
        }

        async Task<Book> IBookRepository.GetBookByIdAsync(int id)
        {
            return await FindByCondition(book => book.BookId == id)
            .FirstOrDefaultAsync();
        }
    }
}

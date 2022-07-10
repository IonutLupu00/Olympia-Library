using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Olympia_Library.Data;

namespace WebApplication.Repositories
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        Task<Book> GetBookByIdAsync(int id);
    }
}

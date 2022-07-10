using Olympia_Library.Data;


namespace WebApplication.Repositories
{
    public class BorrowRepository : RepositoryBase<Borrow>, IBorrowRepository
    {
        public BorrowRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}

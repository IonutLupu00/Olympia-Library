using Olympia_Library.Data;


namespace WebApplication.Repositories
{
    public class GenreRepository : RepositoryBase<Genre>, IGenreRepository
    {
        public GenreRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}

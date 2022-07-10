using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Olympia_Library.Data;


namespace WebApplication.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {

        public ApplicationDbContext _db { get; set; }

        public RepositoryBase(ApplicationDbContext db) 
        {
            _db = db;        
        }

        public void Create(T entity)
        {
            _db.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _db.Set<T>().Update(entity);
        }

        public IQueryable<T> FindAll()
        {
            return this._db.Set<T>();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this._db.Set<T>().Where(expression);
        }

    }
}

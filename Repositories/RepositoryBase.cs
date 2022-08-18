using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected MovieAppDbContext _db { get; set; }
        public RepositoryBase(MovieAppDbContext MovieAppDbContext)
        {
            _db = MovieAppDbContext;
        }

        public IQueryable<T> FindAll() => _db.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            _db.Set<T>().Where(expression).AsNoTracking();

        public bool Exists(Expression<Func<T, bool>> expression) =>
            _db.Set<T>().AsNoTracking().Any(expression);

        public Task<bool> ExistsAsync(Expression<Func<T, bool>> expression) =>
            _db.Set<T>().AsNoTracking().AnyAsync(expression);

        public void Create(T entity) => _db.Set<T>().Add(entity);

        public void Update(T entity) => _db.Set<T>().Update(entity);

        public void Delete(T entity) => _db.Set<T>().Remove(entity);
    }
}
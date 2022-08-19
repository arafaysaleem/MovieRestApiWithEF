using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected MovieAppDbContext DbContext { get; set; }
        public RepositoryBase(MovieAppDbContext MovieAppDbContext)
        {
            DbContext = MovieAppDbContext;
        }

        public IQueryable<T> FindAll() => DbContext.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            DbContext.Set<T>().Where(expression).AsNoTracking();

        public bool Exists(Expression<Func<T, bool>> expression) =>
            DbContext.Set<T>().AsNoTracking().Any(expression);

        public Task<bool> ExistsAsync(Expression<Func<T, bool>> expression) =>
            DbContext.Set<T>().AsNoTracking().AnyAsync(expression);

        public void Create(T entity) => DbContext.Set<T>().Add(entity);

        public void Update(T entity) => DbContext.Set<T>().Update(entity);

        public void Delete(T entity) => DbContext.Set<T>().Remove(entity);
    }
}
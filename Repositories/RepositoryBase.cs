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

        // Setting tracking to false disable change tracking for the returned entities
        // which speeds up read only queries. However, entities without tracking won't be
        // remembered by EF Core and will be inserted as new records instead of updating existing.
        // Therefore in case of updating an entity it should be retrieved with tracking enabled.
        public IQueryable<T> FindAll(bool tracking = default)
        {
            var entities = DbContext.Set<T>();
            return tracking ? entities : entities.AsNoTracking();
        }

        // Setting tracking to false disable change tracking for the returned entities
        // which speeds up read only queries. However, entities without tracking won't be
        // remembered by EF Core and will be inserted as new records instead of updating existing.
        // Therefore in case of updating an entity it should be retrieved with tracking enabled.
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool tracking = default)
        {
            var entities = DbContext.Set<T>().Where(expression);
            return tracking ? entities : entities.AsNoTracking();
        }

        public bool Exists(Expression<Func<T, bool>> expression)
        {
            return DbContext.Set<T>().AsNoTracking().Any(expression);
        }

        public Task<bool> ExistsAsync(Expression<Func<T, bool>> expression)
        {
            return DbContext.Set<T>().AsNoTracking().AnyAsync(expression);
        }

        public void Create(T entity) => DbContext.Set<T>().Add(entity);

        public void Update(T entity) => DbContext.Set<T>().Update(entity);

        public void Delete(T entity) => DbContext.Set<T>().Remove(entity);
    }
}
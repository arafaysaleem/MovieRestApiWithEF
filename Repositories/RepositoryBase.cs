using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DbSet<T> DbSet;
        public RepositoryBase(MovieAppDbContext MovieAppDbContext)
        {
            DbSet = MovieAppDbContext.Set<T>();
        }

        // Setting tracking to false disable change tracking for the returned entities
        // which speeds up read only queries. However, entities without tracking won't be
        // remembered by EF Core and will be inserted as new records instead of updating existing.
        // Therefore in case of updating an entity it should be retrieved with tracking enabled.
        public IQueryable<T> FindAll(bool tracking = default)
        {
            var entities = DbSet;
            return tracking ? entities : entities.AsNoTracking();
        }

        // Setting tracking to false disable change tracking for the returned entities
        // which speeds up read only queries. However, entities without tracking won't be
        // remembered by EF Core and will be inserted as new records instead of updating existing.
        // Therefore in case of updating an entity it should be retrieved with tracking enabled.
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool tracking = default)
        {
            var entities = DbSet.Where(expression);
            return tracking ? entities : entities.AsNoTracking();
        }

        public bool Exists(Expression<Func<T, bool>> expression)
        {
            return DbSet.AsNoTracking().Any(expression);
        }

        public Task<bool> ExistsAsync(Expression<Func<T, bool>> expression)
        {
            return DbSet.AsNoTracking().AnyAsync(expression);
        }

        public void Create(T entity) => DbSet.Add(entity);

        public void Update(T entity) => DbSet.Update(entity);

        public void Delete(T entity) => DbSet.Remove(entity);
    }
}
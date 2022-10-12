using System.Linq.Expressions;

namespace MovieRestApiWithEF.Infrastructure
{
    public interface IRepositoryBase<TEntity>
    {
        IQueryable<TEntity> FindAll(bool tracking = false);
        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, bool tracking = false);
        bool Exists(Expression<Func<TEntity, bool>> expression);
        public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
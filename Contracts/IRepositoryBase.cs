using System.Linq.Expressions;

namespace Contracts
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool tracking = false);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool tracking = false);
        bool Exists(Expression<Func<T, bool>> expression);
        public Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
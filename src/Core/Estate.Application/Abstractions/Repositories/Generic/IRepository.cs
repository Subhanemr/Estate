using Estate.Domain.Entities;
using System.Linq.Expressions;

namespace Estate.Application.Abstractions.Repositories
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        IQueryable<T> GetAll(bool IsTracking = true,
            params string[] includes);

        IQueryable<T> GetAllWhere(Expression<Func<T, bool>>? expression = null,
            int skip = 0,
            int take = 0,
            bool IsTracking = true,
            params string[] includes);

        IQueryable<T> GetAllWhereByOrder(Expression<Func<T, bool>>? expression = null,
            Expression<Func<T, object>>? orderException = null,
            bool IsDescending = false,
            bool IsDeleted = false,
            int skip = 0,
            int take = 0,
            bool IsTracking = true,
            params string[] includes);

        Task<T> GetByIdAsync(int id,
            bool IsTracking = true,
            params string[] includes);

        Task<T> GetByIdPaginatedAsync(int id,
            bool IsTracking = true,
            int take = 0, int skip = 0,
            params string[] includes);

        Task<T> GetByExpressionAsync(Expression<Func<T, bool>> expression,
            bool IsTracking = true,
            params string[] includes);

        Task<double> CountAsync();

        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SoftDelete(T entity);
        void ReverseSoftDelete(T entity);
        Task SaveChanceAsync();
        Task<bool> CheckUniqueAsync(Expression<Func<T, bool>> expression);
    }
}

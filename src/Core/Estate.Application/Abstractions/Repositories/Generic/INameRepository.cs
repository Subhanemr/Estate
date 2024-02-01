using Estate.Domain.Entities;
using System.Linq.Expressions;

namespace Estate.Application.Abstractions.Repositories
{
    public interface INameRepository<T> where T : BaseNameEntity, new()
    {
        IQueryable<T> GetAllBySearch(string? search,
            int take,
            bool IsTracking = true,
            params string[] includes);

        IQueryable<T> GetAllWhereBySearch(string? search,
            Expression<Func<T, bool>>? expression = null,
            int skip = 0,
            int take = 0,
            bool IsTracking = true,
            params string[] includes);

        IQueryable<T> GetAllWhereByOrderBySearch(string? search,
            Expression<Func<T, bool>>? expression = null,
            Expression<Func<T, object>>? orderException = null,
            bool IsDescending = false,
            int skip = 0,
            int take = 0,
            bool IsTracking = true,
            params string[] includes);
    }
}

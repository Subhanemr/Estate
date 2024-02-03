using Estate.Domain.Entities;

namespace Estate.Application.Abstractions.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IQueryable<Category> GetFiltered(string? search, int? order, int skip = 0, int take = 0, params string[] includes);
    }
}

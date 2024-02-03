using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Estate.Persistance.Implementations.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly DbSet<Category> _dbCategory;

        public CategoryRepository(AppDbContext context) : base(context) 
        {
            _dbCategory = context.Set<Category>();
        }

        public IQueryable<Category> GetFiltered(string? search, int? order, int skip = 0, int take = 0, params string[] includes)
        {
            var query = _dbCategory.AsQueryable();

            if (skip != 0) query = query.Skip(skip);

            if (take != 0) query = query.Take(take);

            query = _addIncludes(query, includes);

            switch (order)
            {
                case 1:
                    query = query.OrderBy(p => p.Name);
                    break;
                case 2:
                    query = query.OrderByDescending(p => p.CreateAt);
                    break;
            }
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.ToLower().Contains(search.ToLower()));
            }
            return query;
        }

        private IQueryable<Category> _addIncludes(IQueryable<Category> query, params string[] includes)
        {
            if (includes != null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }
            return query;
        }
    }
}

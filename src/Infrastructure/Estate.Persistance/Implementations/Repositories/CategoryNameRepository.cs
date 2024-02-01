using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;

namespace Estate.Persistance.Implementations.Repositories
{
    public class CategoryNameRepository : NameRepository<Category>, ICategoryNameRepository
    {
        public CategoryNameRepository(AppDbContext context) : base(context) { }
    }
}

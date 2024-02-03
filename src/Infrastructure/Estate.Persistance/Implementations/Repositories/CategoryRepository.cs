using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Estate.Persistance.Implementations.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }
    }
}

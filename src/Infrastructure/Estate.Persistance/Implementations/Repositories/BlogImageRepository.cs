using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;

namespace Estate.Persistance.Implementations.Repositories
{
    public class BlogImageRepository : Repository<BlogImage>, IBlogImageRepository
    {
        public BlogImageRepository(AppDbContext context) : base(context) { }
    }
}

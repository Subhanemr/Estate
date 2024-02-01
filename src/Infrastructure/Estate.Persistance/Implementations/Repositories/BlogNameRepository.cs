using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;

namespace Estate.Persistance.Implementations.Repositories
{
    public class BlogNameRepository : NameRepository<Blog>, IBlogNameRepository
    {
        public BlogNameRepository(AppDbContext context) : base(context) { }
    }
}

using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;

namespace Estate.Persistance.Implementations.Repositories
{
    public class ProductCommentRepository : Repository<ProductComment>, IProductCommentRepository
    {
        public ProductCommentRepository(AppDbContext context) : base(context) { }
    }
}

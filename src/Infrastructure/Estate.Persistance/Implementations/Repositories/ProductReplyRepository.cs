using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;

namespace Estate.Persistance.Implementations.Repositories
{
    public class ProductReplyRepository : Repository<ProductReply>, IProductReplyRepository
    {
        public ProductReplyRepository(AppDbContext context) : base(context) { }
    }
}

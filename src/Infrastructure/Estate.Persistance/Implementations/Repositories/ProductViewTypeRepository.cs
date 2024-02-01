using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;

namespace Estate.Persistance.Implementations.Repositories
{
    public class ProductViewTypeRepository : Repository<ProductViewType>, IProductViewTypeRepository
    {
        public ProductViewTypeRepository(AppDbContext context) : base(context) { }
    }
}

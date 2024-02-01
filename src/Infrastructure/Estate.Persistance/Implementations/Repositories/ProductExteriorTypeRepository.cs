using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;

namespace Estate.Persistance.Implementations.Repositories
{
    public class ProductExteriorTypeRepository : Repository<ProductExteriorType>, IProductExteriorTypeRepository
    {
        public ProductExteriorTypeRepository(AppDbContext context) : base(context) { }
    }
}

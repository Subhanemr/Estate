using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;

namespace Estate.Persistance.Implementations.Repositories
{
    public class ProductRoofTypeRepository : Repository<ProductRoofType>, IProductRoofTypeRepository
    {
        public ProductRoofTypeRepository(AppDbContext context) : base(context) { }
    }
}

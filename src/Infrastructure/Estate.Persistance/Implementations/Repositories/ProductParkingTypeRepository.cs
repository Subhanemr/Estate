using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;

namespace Estate.Persistance.Implementations.Repositories
{
    public class ProductParkingTypeRepository : Repository<ProductParkingType>, IProductParkingTypeRepository
    {
        public ProductParkingTypeRepository(AppDbContext context) : base(context) { }
    }
}

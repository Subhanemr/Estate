using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;

namespace Estate.Persistance.Implementations.Repositories
{
    public class ProductNameRepository : NameRepository<Product>, IProductNameRepository
    {
        public ProductNameRepository(AppDbContext context) : base(context) { }
    }
}

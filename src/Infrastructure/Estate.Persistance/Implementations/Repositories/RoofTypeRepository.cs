using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;

namespace Estate.Persistance.Implementations.Repositories
{
    public class RoofTypeRepository : Repository<RoofType>, IRoofTypeRepository
    {
        public RoofTypeRepository(AppDbContext context) : base(context) { }
    }
}

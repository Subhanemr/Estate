using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;

namespace Estate.Persistance.Implementations.Repositories
{
    public class RoofTypeNameRepository : NameRepository<RoofType>, IRoofTypeNameRepository
    {
        public RoofTypeNameRepository(AppDbContext context) : base(context) { }

    }
}

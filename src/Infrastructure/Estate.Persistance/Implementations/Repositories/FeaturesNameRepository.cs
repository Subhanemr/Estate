using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;

namespace Estate.Persistance.Implementations.Repositories
{
    public class FeaturesNameRepository : NameRepository<Features>, IFeaturesNameRepository
    {
        public FeaturesNameRepository(AppDbContext context) : base(context) { }
    }
}

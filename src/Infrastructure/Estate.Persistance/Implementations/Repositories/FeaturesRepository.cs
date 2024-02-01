using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;

namespace Estate.Persistance.Implementations.Repositories
{
    internal class FeaturesRepository : Repository<Features>, IFeaturesRepository
    {
        public FeaturesRepository(AppDbContext context) : base(context) { }

    }
}

using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;

namespace Estate.Persistance.Implementations.Repositories
{
    public class AgencyRepository : Repository<Agency>, IAgencyRepository
    {
        public AgencyRepository(AppDbContext context) : base(context) { }
    }
}


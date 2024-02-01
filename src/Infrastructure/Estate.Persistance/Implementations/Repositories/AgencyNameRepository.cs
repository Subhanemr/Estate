using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;

namespace Estate.Persistance.Implementations.Repositories
{
    public class AgencyNameRepository : NameRepository<Agency>, IAgencyNameRepository
    {
        public AgencyNameRepository(AppDbContext context) : base(context) { }
    }
}

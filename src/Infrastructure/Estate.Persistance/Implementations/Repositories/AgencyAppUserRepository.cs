using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;

namespace Estate.Persistance.Implementations.Repositories
{
    public class AgencyAppUserRepository : Repository<AgencyAppUser>, IAgencyAppUserRepository
    {
        public AgencyAppUserRepository(AppDbContext context) : base(context) { }
    }
}

using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;

namespace Estate.Persistance.Implementations.Repositories
{
    public class CorporateRepository : Repository<Corporate>, ICorporateRepository
    {
        public CorporateRepository(AppDbContext context) : base(context) { }
    }
}

using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;

namespace Estate.Persistance.Implementations.Repositories
{
    public class CorporateNameRepository : NameRepository<Corporate>, ICorporateNameRepository
    {
        public CorporateNameRepository(AppDbContext context) : base(context) { }
    }
}

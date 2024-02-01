using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;

namespace Estate.Persistance.Implementations.Repositories
{
    public class ExteriorTypeNameRepository : NameRepository<ExteriorType>, IExteriorTypeNameRepository
    {
        public ExteriorTypeNameRepository(AppDbContext context) : base(context) { }
    }
}

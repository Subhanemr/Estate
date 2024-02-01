using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;

namespace Estate.Persistance.Implementations.Repositories
{
    internal class ExteriorTypeRepository : Repository<ExteriorType>, IExteriorTypeRepository
    {
        public ExteriorTypeRepository(AppDbContext context) : base(context) { }
    }
}

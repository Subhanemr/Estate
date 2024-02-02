using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;

namespace Estate.Persistance.Implementations.Repositories
{
    public class ParkingTypeRepository : Repository<ParkingType>, IParkingTypeRepository
    {
        public ParkingTypeRepository(AppDbContext context) : base(context) { }
    }
}

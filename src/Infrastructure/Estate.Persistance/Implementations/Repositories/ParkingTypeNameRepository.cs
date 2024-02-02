using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;

namespace Estate.Persistance.Implementations.Repositories
{
    public class ParkingTypeNameRepository : NameRepository<ParkingType>, IParkingTypeNameRepository
    {
        public ParkingTypeNameRepository(AppDbContext context) : base(context) { }
    }
}

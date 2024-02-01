using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;

namespace Estate.Persistance.Implementations.Repositories
{
    public class AppUserImageRepository : Repository<AppUserImage>, IAppUserImageRepository
    {
        public AppUserImageRepository(AppDbContext context) : base(context) { }
    }
}

using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;

namespace Estate.Persistance.Implementations.Repositories
{
    public class ClientNameRepository : NameRepository<Client>, IClientNameRepository
    {
        public ClientNameRepository(AppDbContext context) : base(context) { }
    }
}

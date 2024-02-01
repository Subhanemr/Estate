using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;

namespace Estate.Persistance.Implementations.Repositories
{
    public class ViewTypeRepository : Repository<ViewType>, IViewTypeRepository
    {
        public ViewTypeRepository(AppDbContext context) : base(context) { }

    }
}

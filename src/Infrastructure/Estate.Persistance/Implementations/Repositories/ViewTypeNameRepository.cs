using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;

namespace Estate.Persistance.Implementations.Repositories
{
    public class ViewTypeNameRepository : NameRepository<ViewType>, IViewTypeNameRepository
    {
        public ViewTypeNameRepository(AppDbContext context) : base(context) { }

    }
}

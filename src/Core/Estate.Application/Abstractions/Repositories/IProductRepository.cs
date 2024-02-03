using Estate.Domain.Entities;

namespace Estate.Application.Abstractions.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        void DeleteExteriorType(ProductExteriorType item);
        void DeleteFeatures(ProductFeatures item);
        void DeleteRoofType(ProductRoofType item);
        void DeleteParkingType(ProductParkingType item);
        void DeleteViewType(ProductViewType item);
        IQueryable<Product> GetFiltered(string? search, int? order, int? categoryId, int skip = 0, int take = 0, params string[] includes);
    }
}

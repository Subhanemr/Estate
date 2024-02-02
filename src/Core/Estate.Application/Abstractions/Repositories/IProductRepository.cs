using Estate.Domain.Entities;

namespace Estate.Application.Abstractions.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        void DeleteImage(ProductImage image);
        Task AddComment(ProductComment comment);
        void UpdateComment(ProductComment comment);
        void DeleteComment(ProductComment comment);
        Task AddReply(ProductReply comment);
        void UpdateReply(ProductReply comment);
        void DeleteReply(ProductReply comment);
        void DeleteExteriorType(ProductExteriorType item);
        void DeleteFeatures(ProductFeatures item);
        void DeleteRoofType(ProductRoofType item);
        void DeleteParkingType(ProductParkingType item);
        void DeleteViewType(ProductViewType item);
    }
}

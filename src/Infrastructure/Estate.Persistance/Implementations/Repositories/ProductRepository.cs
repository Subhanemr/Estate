using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Estate.Persistance.Implementations.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly DbSet<ProductImage> _dbImage;
        private readonly DbSet<ProductComment> _dbComment;
        private readonly DbSet<ProductReply> _dbReply;
        private readonly DbSet<ProductExteriorType> _dbExteriorType;
        private readonly DbSet<ProductFeatures> _dbFeatures;
        private readonly DbSet<ProductParkingType> _dbParkingType;
        private readonly DbSet<ProductRoofType> _dbRoofType;
        private readonly DbSet<ProductViewType> _dbViewType;

        public ProductRepository(AppDbContext context) : base(context)
        {
            _dbImage = context.Set<ProductImage>();
            _dbComment = context.Set<ProductComment>();
            _dbReply = context.Set<ProductReply>();
        }

        public void DeleteImage(ProductImage image)
        {
            _dbImage.Remove(image);
        }
        public async Task AddComment(ProductComment comment)
        {
            await _dbComment.AddAsync(comment);
        }
        public void UpdateComment(ProductComment comment)
        {
            _dbComment.Update(comment);
        }
        public void DeleteComment(ProductComment comment)
        {
            _dbComment.Remove(comment);
        }
        public async Task AddReply(ProductReply comment)
        {
            await _dbReply.AddAsync(comment);
        }
        public void UpdateReply(ProductReply comment)
        {
            _dbReply.Update(comment);
        }
        public void DeleteReply(ProductReply comment)
        {
            _dbReply.Remove(comment);
        }
        public void DeleteExteriorType(ProductExteriorType item)
        {
            _dbExteriorType.Remove(item);
        }
        public void DeleteFeatures(ProductFeatures item)
        {
            _dbFeatures.Remove(item);
        }
        public void DeleteRoofType(ProductRoofType item)
        {
            _dbRoofType.Remove(item);
        }
        public void DeleteParkingType(ProductParkingType item)
        {
            _dbParkingType.Remove(item);
        }
        public void DeleteViewType(ProductViewType item)
        {
            _dbViewType.Remove(item);
        }
    }
}

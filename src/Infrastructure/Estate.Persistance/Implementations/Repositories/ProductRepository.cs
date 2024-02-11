using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Estate.Persistance.Implementations.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly DbSet<ProductExteriorType> _dbExteriorType;
        private readonly DbSet<ProductFeatures> _dbFeatures;
        private readonly DbSet<ProductParkingType> _dbParkingType;
        private readonly DbSet<ProductRoofType> _dbRoofType;
        private readonly DbSet<ProductViewType> _dbViewType;
        private readonly DbSet<ProductComment> _dbproductComments;
        private readonly DbSet<ProductReply> _dbproductReplies;


        public ProductRepository(AppDbContext context) : base(context)
        {
            _dbExteriorType = context.Set<ProductExteriorType>();
            _dbFeatures = context.Set<ProductFeatures>();
            _dbParkingType = context.Set<ProductParkingType>();
            _dbRoofType = context.Set<ProductRoofType>();
            _dbViewType = context.Set<ProductViewType>();
            _dbproductComments = context.Set<ProductComment>();
            _dbproductReplies = context.Set<ProductReply>();

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
        public async Task AddComment(ProductComment item)
        {
            await _dbproductComments.AddAsync(item);
        }
        public async Task AddReply(ProductReply item)
        {
            await _dbproductReplies.AddAsync(item);
        }
    }
}

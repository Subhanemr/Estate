using Estate.Application.Abstractions.Repositories;
using Estate.Application.ViewModels.Product;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Estate.Persistance.Implementations.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly DbSet<ProductExteriorType> _dbExteriorType;
        private readonly DbSet<ProductFeatures> _dbFeatures;
        private readonly DbSet<ProductParkingType> _dbParkingType;
        private readonly DbSet<ProductRoofType> _dbRoofType;
        private readonly DbSet<ProductViewType> _dbViewType;
        private readonly DbSet<Product> _dbProduct;

        public ProductRepository(AppDbContext context) : base(context)
        {
            _dbExteriorType = context.Set<ProductExteriorType>();
            _dbFeatures = context.Set<ProductFeatures>();
            _dbParkingType = context.Set<ProductParkingType>();
            _dbRoofType = context.Set<ProductRoofType>();
            _dbViewType = context.Set<ProductViewType>();
            _dbProduct = context.Set<Product>();
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
        public IQueryable<Product> GetFiltered(string? search, int? order, int? categoryId, int skip = 0, int take = 0, params string[] includes)
        {
            var query = _dbProduct.AsQueryable();

            if (skip != 0) query = query.Skip(skip);

            if (take != 0) query = query.Take(take);

            query = _addIncludes(query, includes);
            
            switch (order)
            {
                case 1:
                    query = query.OrderBy(p => p.Name);
                    break;
                case 2:
                    query = query.OrderBy(p => p.Price);
                    break;
                case 3:
                    query = query.OrderByDescending(p => p.CreateAt);
                    break;
                case 4:
                    query = query.OrderByDescending(p => p.Price);
                    break;
            }
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.ToLower().Contains(search.ToLower()));
            }
            if (categoryId != null)
            {
                query = query.Where(p => p.CategoryId == categoryId);
            }
            return query;
        }

        private IQueryable<Product> _addIncludes(IQueryable<Product> query, params string[] includes)
        {
            if (includes != null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }
            return query;
        }
    }
}

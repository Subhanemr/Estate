﻿using Estate.Domain.Entities;

namespace Estate.Application.Abstractions.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        void DeleteExteriorType(ProductExteriorType item);
        void DeleteFeatures(ProductFeatures item);
        void DeleteRoofType(ProductRoofType item);
        void DeleteParkingType(ProductParkingType item);
        void DeleteViewType(ProductViewType item);
    }
}

﻿using Estate.Application.Abstractions.Repositories;
using Estate.Domain.Entities;
using Estate.Persistance.Contexts;

namespace Estate.Persistance.Implementations.Repositories
{
    public class ProductFeaturesRepository : Repository<ProductFeatures>, IProductFeaturesRepository
    {
        public ProductFeaturesRepository(AppDbContext context) : base(context) { }
    }
}
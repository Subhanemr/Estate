using AutoMapper;
using Estate.Application.Abstractions.Repositories;
using Estate.Application.Abstractions.Services;
using Estate.Application.ViewModels;
using Estate.Domain.Entities;
using Estate.Infrastructure.Exceptions;
using Estate.Infrastructure.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Estate.Persistance.Implementations.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IEmailService _email;
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IFeaturesRepository _featuresRepository;
        private readonly IExteriorTypeRepository _exteriorTypeRepository;
        private readonly IParkingTypeRepository _parkingTypeRepository;
        private readonly IRoofTypeRepository _roofTypeRepository;
        private readonly IViewTypeRepository _viewTypeRepository;
        private readonly ICLoudService _cLoud;
        private readonly IHttpContextAccessor _http;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<AppUser> _userManager;

        public ProductService(IMapper mapper, IProductRepository repository,
            ICategoryRepository categoryRepository, IFeaturesRepository featuresRepository, IExteriorTypeRepository exteriorTypeRepository,
            IParkingTypeRepository parkingTypeRepository, IRoofTypeRepository roofTypeRepository, IViewTypeRepository viewTypeRepository,
            IHttpContextAccessor http, IWebHostEnvironment env, UserManager<AppUser> userManager, ICLoudService cLoud, IEmailService email)
        {
            _mapper = mapper;
            _repository = repository;
            _categoryRepository = categoryRepository;
            _featuresRepository = featuresRepository;
            _exteriorTypeRepository = exteriorTypeRepository;
            _parkingTypeRepository = parkingTypeRepository;
            _roofTypeRepository = roofTypeRepository;
            _viewTypeRepository = viewTypeRepository;
            _http = http;
            _env = env;
            _userManager = userManager;
            _cLoud = cLoud;
            _email = email;
        }

        public async Task CreatePopulateDropdowns(CreateProductVM create)
        {
            create.Categories = _mapper.Map<ICollection<IncludeCategoryVM>>(await _categoryRepository.GetAll().ToListAsync());
            create.Features = _mapper.Map<ICollection<IncludeFeaturesVM>>(await _featuresRepository.GetAll().ToListAsync());
            create.ExteriorTypes = _mapper.Map<ICollection<IncludeExteriorTypeVM>>(await _exteriorTypeRepository.GetAll().ToListAsync());
            create.ParkingTypes = _mapper.Map<ICollection<IncludeParkingTypeVM>>(await _parkingTypeRepository.GetAll().ToListAsync());
            create.RoofTypes = _mapper.Map<ICollection<IncludeRoofTypeVM>>(await _roofTypeRepository.GetAll().ToListAsync());
            create.ViewTypes = _mapper.Map<ICollection<IncludeViewTypeVM>>(await _viewTypeRepository.GetAll().ToListAsync());
        }
        public async Task UpdatePopulateDropdowns(UpdateProductVM update)
        {
            update.Categories = _mapper.Map<ICollection<IncludeCategoryVM>>(await _categoryRepository.GetAll().ToListAsync());
            update.Features = _mapper.Map<ICollection<IncludeFeaturesVM>>(await _featuresRepository.GetAll().ToListAsync());
            update.ExteriorTypes = _mapper.Map<ICollection<IncludeExteriorTypeVM>>(await _exteriorTypeRepository.GetAll().ToListAsync());
            update.ParkingTypes = _mapper.Map<ICollection<IncludeParkingTypeVM>>(await _parkingTypeRepository.GetAll().ToListAsync());
            update.RoofTypes = _mapper.Map<ICollection<IncludeRoofTypeVM>>(await _roofTypeRepository.GetAll().ToListAsync());
            update.ViewTypes = _mapper.Map<ICollection<IncludeViewTypeVM>>(await _viewTypeRepository.GetAll().ToListAsync());
        }

        public async Task<bool> CreateAsync(CreateProductVM create, ModelStateDictionary model, ITempDataDictionary tempData)
        {
            if (!model.IsValid)
            {
                await CreatePopulateDropdowns(create);
                return false;
            }

            if (!await _categoryRepository.CheckUniqueAsync(x => x.Id == create.CategoryId))
            {
                await CreatePopulateDropdowns(create);
                return false;
            }
            foreach (int featureId in create.FeatureIds)
            {
                if (!await _featuresRepository.CheckUniqueAsync(x => x.Id == featureId))
                {
                    await CreatePopulateDropdowns(create);
                    return false;
                }
            }
            foreach (int exteriorTypeId in create.ExteriorTypeIds)
            {
                if (!await _exteriorTypeRepository.CheckUniqueAsync(x => x.Id == exteriorTypeId))
                {
                    await CreatePopulateDropdowns(create);
                    return false;
                }
            }
            foreach (int parkingTypeId in create.ParkingTypeIds)
            {
                if (!await _parkingTypeRepository.CheckUniqueAsync(x => x.Id == parkingTypeId))
                {
                    await CreatePopulateDropdowns(create);
                    return false;
                }
            }
            foreach (int roofTypeId in create.RoofTypeIds)
            {
                if (!await _roofTypeRepository.CheckUniqueAsync(x => x.Id == roofTypeId))
                {
                    await CreatePopulateDropdowns(create);
                    return false;
                }
            }
            foreach (int viewTypeId in create.ViewTypeIds)
            {
                if (!await _viewTypeRepository.CheckUniqueAsync(x => x.Id == viewTypeId))
                {
                    await CreatePopulateDropdowns(create);
                    return false;
                }
            }
            if (!create.MainPhoto.ValidateType())
            {
                await CreatePopulateDropdowns(create);
                model.AddModelError("MainPhoto", "File Not supported");
                return false;
            }
            if (!create.MainPhoto.ValidataSize())
            {
                await CreatePopulateDropdowns(create);
                model.AddModelError("MainPhoto", "Image should not be larger than 10 mb");
                return false;
            }
            ProductImage mainImage = new ProductImage
            {
                //CreatedBy = _http.HttpContext.User.Identity.Name,
                IsPrimary = true,
                Url = await _cLoud.FileCreateAsync(create.MainPhoto)
                //Url = await create.MainPhoto.CreateFileAsync(_env.WebRootPath, "assets", "images")
            };

            Product item = _mapper.Map<Product>(create);

            item.ProductImages = new List<ProductImage> { mainImage };
            item.ProductFeatures = create.FeatureIds.Select(id => new ProductFeatures { FeaturesId = id }).ToList();
            item.ProductExteriorTypes = create.ExteriorTypeIds.Select(id => new ProductExteriorType { ExteriorTypeId = id }).ToList();
            item.ProductParkingTypes = create.ParkingTypeIds.Select(id => new ProductParkingType { ParkingTypeId = id }).ToList();
            item.ProductRoofTypes = create.RoofTypeIds.Select(id => new ProductRoofType { RoofTypeId = id }).ToList();
            item.ProductViewTypes = create.ViewTypeIds.Select(id => new ProductViewType { ViewTypeId = id }).ToList();


            if (item.ProductImages == null) item.ProductImages = new List<ProductImage>();

            tempData["Message"] = "";

            foreach (IFormFile photo in create.Photos)
            {
                if (!photo.ValidateType())
                {
                    tempData["Message"] += $"<h5 class=\"text-danger\" style=\"margin-left: 100px; color: red;\">{photo.Name} type is not suitable</h5>";
                    continue;
                }

                if (!photo.ValidataSize(10))
                {
                    tempData["Message"] += $"<h5 class=\"text-danger\" style=\"margin-left: 100px; color: red;\">{photo.Name} the size is not suitable</h5>";
                    continue;
                }

                item.ProductImages.Add(new ProductImage
                {
                    //CreatedBy = _http.HttpContext.User.Identity.Name,
                    IsPrimary = null,
                    Url = await _cLoud.FileCreateAsync(photo)
                    //Url = await photo.CreateFileAsync(_env.WebRootPath, "assets", "images")
                });
            }
            AppUser user = await _userManager.FindByNameAsync(_http.HttpContext.User.Identity.Name);
            item.AppUserId = user.Id;
            //item.CreatedBy = user.UserName;

            await _repository.AddAsync(item);
            await _repository.SaveChangeAsync();

            return true;
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes ={
                $"{nameof(Product.Category)}",
                $"{nameof(Product.ProductComments)}.{nameof(ProductComment.ProductReplies)}",
                $"{nameof(Product.ProductFeatures)}.{nameof(ProductFeatures.Features)}",
                $"{nameof(Product.ProductExteriorTypes)}.{nameof(ProductExteriorType.ExteriorType)}",
                $"{nameof(Product.ProductParkingTypes)}.{nameof(ProductParkingType.ParkingType)}",
                $"{nameof(Product.ProductRoofTypes)}.{nameof(ProductRoofType.RoofType)}",
                $"{nameof(Product.ProductViewTypes)}.{nameof(ProductViewType.ViewType)}",
                $"{nameof(Product.ProductImages)}" };
            Product item = await _repository.GetByIdAsync(id, includes: includes);
            if (item == null) throw new NotFoundException("Your request was not found");
            foreach (var image in item.ProductImages)
            {
                await _cLoud.FileDeleteAsync(image.Url);
                //image.Url.DeleteFile(_env.WebRootPath, "assets", "images");
            }
            _repository.Delete(item);
            await _repository.SaveChangeAsync();
        }

        public async Task<ICollection<ItemProductVM>> GetAllWhereAsync(int take, int page = 1)
        {
            string[] includes ={
                $"{nameof(Product.Category)}",
                $"{nameof(Product.ProductImages)}" };
            ICollection<Product> items = await _repository
                    .GetAllWhere(skip: (page - 1) * take, take: take, IsTracking: false, includes: includes).ToListAsync();

            ICollection<ItemProductVM> vMs = _mapper.Map<ICollection<ItemProductVM>>(items);

            return vMs;
        }

        public async Task<ICollection<ItemProductVM>> GetAllWhereByOrderAsync(int take, Expression<Func<Product, object>>? orderExpression, int page)
        {
            string[] includes ={
                $"{nameof(Product.Category)}",
                $"{nameof(Product.ProductImages)}" };
            ICollection<Product> items = await _repository
                    .GetAllWhereByOrder(orderException: orderExpression, skip: (page - 1) * take, take: take, IsTracking: false, includes: includes).ToListAsync();

            ICollection<ItemProductVM> vMs = _mapper.Map<ICollection<ItemProductVM>>(items);

            return vMs;
        }
        public async Task<PaginationVM<ProductFilterVM>> GetAllWhereByOrderFilterAsync(int take, int page, Expression<Func<Product, object>>? orderExpression)
        {
            string[] includes ={
                $"{nameof(Product.Category)}",
                $"{nameof(Product.ProductComments)}",
                $"{nameof(Product.ProductImages)}" };
            ICollection<Product> items = await _repository
                    .GetAllWhereByOrder(orderException: orderExpression, skip: (page - 1) * take, take: take, IsTracking: false, includes: includes).ToListAsync();

            ICollection<ItemProductVM> vMs = _mapper.Map<ICollection<ItemProductVM>>(items);

            ProductFilterVM filtered = new ProductFilterVM
            {
                Products = _mapper.Map<ICollection<ItemProductVM>>(items),
                Categories = _mapper.Map<ICollection<IncludeCategoryVM>>(await _categoryRepository.GetAllWhereByOrder(orderException: x => x.Products.Count).ToListAsync())
            };
            PaginationVM<ProductFilterVM> pagination = new PaginationVM<ProductFilterVM>
            {
                Take = take,
                Item = filtered
            };

            return pagination;
        }
        public async Task<ICollection<ItemProductVM>> GetAllWhereByBoolAsync(int take, Expression<Func<Product, bool>>? expression, int page)
        {
            string[] includes ={
                $"{nameof(Product.Category)}",
                $"{nameof(Product.ProductImages)}" };
            ICollection<Product> items = await _repository
                    .GetAllWhereByOrder(expression: expression, skip: (page - 1) * take, take: take, IsTracking: false, includes: includes).ToListAsync();

            ICollection<ItemProductVM> vMs = _mapper.Map<ICollection<ItemProductVM>>(items);

            return vMs;
        }
        public async Task<PaginationVM<ProductFilterVM>> GetFilteredAsync(string? search, int take, int page, int order, 
            int? categoryId, int? minPrice, int? maxPrice, int? minArea, int? maxArea, int? minBeds, int? minBaths)
        {
            if (page <= 0) throw new WrongRequestException("The request sent does not exist");
            if (order <= 0) throw new WrongRequestException("The request sent does not exist");

            string[] includes ={
                $"{nameof(Product.Category)}",
                $"{nameof(Product.ProductComments)}",
                $"{nameof(Product.ProductImages)}" };
            double count = await _repository.CountAsync();

            ICollection<Product> items = new List<Product>();
            switch (order)
            {
                case 1:
                    items = await _repository
                        .GetAllWhereByOrder(
                            x => (categoryId != null ? x.CategoryId == categoryId : true)
                                && (minPrice != null ? x.Price >= minPrice : true)
                                && (maxPrice != null ? x.Price <= maxPrice : true)
                                && (minArea != null ? x.Area >= minArea : true)
                                && (maxArea != null ? x.Area <= maxArea : true)
                                && (minBeds != null ? x.Bedrooms >= minBeds : true)
                                && (minBaths != null ? x.Bathrooms >= minBaths : true)
                                && (!string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true),
                            x => x.Name, false, false, (page - 1) * take, take, false, includes)
                        .ToListAsync();

                    break;
                case 2:
                    items = await _repository
                     .GetAllWhereByOrder(x => (categoryId != null ? x.CategoryId == categoryId : true)
                                && (minPrice != null ? x.Price >= minPrice : true)
                                && (maxPrice != null ? x.Price <= maxPrice : true)
                                && (minArea != null ? x.Area >= minArea : true)
                                && (maxArea != null ? x.Area <= maxArea : true)
                                && (minBeds != null ? x.Bedrooms >= minBeds : true)
                                && (minBaths != null ? x.Bathrooms >= minBaths : true)
                                && (!string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true),
                      x => x.CreateAt, false, false, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 3:
                    items = await _repository
                    .GetAllWhereByOrder(x => (categoryId != null ? x.CategoryId == categoryId : true)
                                && (minPrice != null ? x.Price >= minPrice : true)
                                && (maxPrice != null ? x.Price <= maxPrice : true)
                                && (minArea != null ? x.Area >= minArea : true)
                                && (maxArea != null ? x.Area <= maxArea : true)
                                && (minBeds != null ? x.Bedrooms >= minBeds : true)
                                && (minBaths != null ? x.Bathrooms >= minBaths : true)
                                && (!string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true),
                        x => x.Name, true, false, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 4:
                    items = await _repository
                     .GetAllWhereByOrder(x => (categoryId != null ? x.CategoryId == categoryId : true)
                                && (minPrice != null ? x.Price >= minPrice : true)
                                && (maxPrice != null ? x.Price <= maxPrice : true)
                                && (minArea != null ? x.Area >= minArea : true)
                                && (maxArea != null ? x.Area <= maxArea : true)
                                && (minBeds != null ? x.Bedrooms >= minBeds : true)
                                && (minBaths != null ? x.Bathrooms >= minBaths : true)
                                && (!string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true),
                      x => x.CreateAt, true, false, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 5:
                    items = await _repository
                     .GetAllWhereByOrder(x => (categoryId != null ? x.CategoryId == categoryId : true)
                                && (minPrice != null ? x.Price >= minPrice : true)
                                && (maxPrice != null ? x.Price <= maxPrice : true)
                                && (minArea != null ? x.Area >= minArea : true)
                                && (maxArea != null ? x.Area <= maxArea : true)
                                && (minBeds != null ? x.Bedrooms >= minBeds : true)
                                && (minBaths != null ? x.Bathrooms >= minBaths : true)
                                && (!string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true),
                      x => x.Price, false, false, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 6:
                    items = await _repository
                     .GetAllWhereByOrder(x => (categoryId != null ? x.CategoryId == categoryId : true)
                                && (minPrice != null ? x.Price >= minPrice : true)
                                && (maxPrice != null ? x.Price <= maxPrice : true)
                                && (minArea != null ? x.Area >= minArea : true)
                                && (maxArea != null ? x.Area <= maxArea : true)
                                && (minBeds != null ? x.Bedrooms >= minBeds : true)
                                && (minBaths != null ? x.Bathrooms >= minBaths : true)
                                && (!string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true),
                      x => x.Price, true, false, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
            }

            ProductFilterVM filtered = new ProductFilterVM
            {
                Products = _mapper.Map<ICollection<ItemProductVM>>(items),
                Categories = _mapper.Map<ICollection<IncludeCategoryVM>>(await _categoryRepository.GetAll(false).ToListAsync())
            };
            PaginationVM<ProductFilterVM> pagination = new PaginationVM<ProductFilterVM>
            {
                Take = take,
                Search = search,
                Order = order,
                CategoryId = categoryId,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                MinArea = minArea,
                MaxArea = maxArea,
                MinBaths = minBaths,
                MinBeds = minBeds,
                CurrentPage = page,
                TotalPage = Math.Ceiling(count / take),
                Item = filtered
            };

            return pagination;
        }

        public async Task<PaginationVM<ProductFilterVM>> GetDeleteFilteredAsync(string? search, int take, int page, int order, 
            int? categoryId, int? minPrice, int? maxPrice, int? minArea, int? maxArea, int? minBeds, int? minBaths)
        {
            if (page <= 0) throw new WrongRequestException("The request sent does not exist");
            if (order <= 0) throw new WrongRequestException("The request sent does not exist");

            string[] includes ={
                $"{nameof(Product.Category)}",
                $"{nameof(Product.ProductComments)}",
                $"{nameof(Product.ProductImages)}" };
            double count = await _repository.CountAsync();

            ICollection<Product> items = new List<Product>();

            switch (order)
            {
                case 1:
                    items = await _repository
                    .GetAllWhereByOrder(x => (categoryId != null ? x.CategoryId == categoryId : true)
                                && (minPrice != null ? x.Price >= minPrice : true)
                                && (maxPrice != null ? x.Price <= maxPrice : true)
                                && (minArea != null ? x.Area >= minArea : true)
                                && (maxArea != null ? x.Area <= maxArea : true)
                                && (minBeds != null ? x.Bedrooms >= minBeds : true)
                                && (minBaths != null ? x.Bathrooms >= minBaths : true)
                                && (!string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true),
                        x => x.Name, false, true, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 2:
                    items = await _repository
                     .GetAllWhereByOrder(x => (categoryId != null ? x.CategoryId == categoryId : true)
                                && (minPrice != null ? x.Price >= minPrice : true)
                                && (maxPrice != null ? x.Price <= maxPrice : true)
                                && (minArea != null ? x.Area >= minArea : true)
                                && (maxArea != null ? x.Area <= maxArea : true)
                                && (minBeds != null ? x.Bedrooms >= minBeds : true)
                                && (minBaths != null ? x.Bathrooms >= minBaths : true)
                                && (!string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true),
                      x => x.CreateAt, false, true, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 3:
                    items = await _repository
                    .GetAllWhereByOrder(x => (categoryId != null ? x.CategoryId == categoryId : true)
                                && (minPrice != null ? x.Price >= minPrice : true)
                                && (maxPrice != null ? x.Price <= maxPrice : true)
                                && (minArea != null ? x.Area >= minArea : true)
                                && (maxArea != null ? x.Area <= maxArea : true)
                                && (minBeds != null ? x.Bedrooms >= minBeds : true)
                                && (minBaths != null ? x.Bathrooms >= minBaths : true)
                                && (!string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true),
                        x => x.Name, true, true, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 4:
                    items = await _repository
                     .GetAllWhereByOrder(x => (categoryId != null ? x.CategoryId == categoryId : true)
                                && (minPrice != null ? x.Price >= minPrice : true)
                                && (maxPrice != null ? x.Price <= maxPrice : true)
                                && (minArea != null ? x.Area >= minArea : true)
                                && (maxArea != null ? x.Area <= maxArea : true)
                                && (minBeds != null ? x.Bedrooms >= minBeds : true)
                                && (minBaths != null ? x.Bathrooms >= minBaths : true)
                                && (!string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true),
                      x => x.CreateAt, true, true, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 5:
                    items = await _repository
                     .GetAllWhereByOrder(x => (categoryId != null ? x.CategoryId == categoryId : true)
                                && (minPrice != null ? x.Price >= minPrice : true)
                                && (maxPrice != null ? x.Price <= maxPrice : true)
                                && (minArea != null ? x.Area >= minArea : true)
                                && (maxArea != null ? x.Area <= maxArea : true)
                                && (minBeds != null ? x.Bedrooms >= minBeds : true)
                                && (minBaths != null ? x.Bathrooms >= minBaths : true)
                                && (!string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true),
                      x => x.Price, false, true, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 6:
                    items = await _repository
                     .GetAllWhereByOrder(x => (categoryId != null ? x.CategoryId == categoryId : true)
                                && (minPrice != null ? x.Price >= minPrice : true)
                                && (maxPrice != null ? x.Price <= maxPrice : true)
                                && (minArea != null ? x.Area >= minArea : true)
                                && (maxArea != null ? x.Area <= maxArea : true)
                                && (minBeds != null ? x.Bedrooms >= minBeds : true)
                                && (minBaths != null ? x.Bathrooms >= minBaths : true)
                                && (!string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true),
                      x => x.Price, true, true, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
            }

            ProductFilterVM filtered = new ProductFilterVM
            {
                Products = _mapper.Map<ICollection<ItemProductVM>>(items),
                Categories = _mapper.Map<ICollection<IncludeCategoryVM>>(await _categoryRepository.GetAll(false).ToListAsync()),
            };
            PaginationVM<ProductFilterVM> pagination = new PaginationVM<ProductFilterVM>
            {
                Take = take,
                Search = search,
                Order = order,
                CategoryId = categoryId,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                MinArea = minArea,
                MaxArea = maxArea,
                MinBaths = minBaths,
                MinBeds = minBeds,
                CurrentPage = page,
                TotalPage = Math.Ceiling(count / take),
                Item = filtered
            };

            return pagination;
        }

        public async Task<GetProductVM> GetByIdAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes ={
                $"{nameof(Product.ProductComments)}.{nameof(ProductComment.ProductReplies)}.{nameof(ProductReply.AppUser)}",
                $"{nameof(Product.ProductComments)}.{nameof(ProductComment.AppUser)}",
                $"{nameof(Product.Category)}",
                $"{nameof(Product.ProductFeatures)}.{nameof(ProductFeatures.Features)}",
                $"{nameof(Product.ProductExteriorTypes)}.{nameof(ProductExteriorType.ExteriorType)}",
                $"{nameof(Product.ProductParkingTypes)}.{nameof(ProductParkingType.ParkingType)}",
                $"{nameof(Product.ProductRoofTypes)}.{nameof(ProductRoofType.RoofType)}",
                $"{nameof(Product.ProductViewTypes)}.{nameof(ProductViewType.ViewType)}",
                $"{nameof(Product.AppUser)}",
                $"{nameof(Product.ProductImages)}" };
            Product item = await _repository.GetByIdAsync(id, includes: includes);
            if (item == null) throw new NotFoundException("Your request was not found");

            GetProductVM get = _mapper.Map<GetProductVM>(item);

            return get;
        }

        public async Task ReverseSoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Product item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            item.IsDeleted = false;
            await _repository.SaveChangeAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Product item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            item.IsDeleted = true;
            await _repository.SaveChangeAsync();
        }

        public async Task<bool> UpdatePostAsync(int id, UpdateProductVM update, ModelStateDictionary model, ITempDataDictionary tempData)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes ={
                $"{nameof(Product.Category)}",
                $"{nameof(Product.ProductFeatures)}.{nameof(ProductFeatures.Features)}",
                $"{nameof(Product.ProductExteriorTypes)}.{nameof(ProductExteriorType.ExteriorType)}",
                $"{nameof(Product.ProductParkingTypes)}.{nameof(ProductParkingType.ParkingType)}",
                $"{nameof(Product.ProductRoofTypes)}.{nameof(ProductRoofType.RoofType)}",
                $"{nameof(Product.ProductViewTypes)}.{nameof(ProductViewType.ViewType)}",
                $"{nameof(Product.ProductImages)}" };
            Product item = await _repository.GetByIdAsync(id, includes: includes);
            update.Images = _mapper.Map<ICollection<IncludeProductImageVM>>(item.ProductImages);
            if (item == null) throw new NotFoundException("Your request was not found");

            if (!model.IsValid)
            {
                await UpdatePopulateDropdowns(update);
                return false;
            }

            if (!await _categoryRepository.CheckUniqueAsync(x => x.Id == update.CategoryId))
            {
                await UpdatePopulateDropdowns(update);
                return false;
            }
            foreach (int featureId in update.FeatureIds)
            {
                if (!await _featuresRepository.CheckUniqueAsync(x => x.Id == featureId))
                {
                    await UpdatePopulateDropdowns(update);
                    return false;
                }
            }
            foreach (int exteriorTypeId in update.ExteriorTypeIds)
            {
                if (!await _exteriorTypeRepository.CheckUniqueAsync(x => x.Id == exteriorTypeId))
                {
                    await UpdatePopulateDropdowns(update);
                    return false;
                }
            }
            foreach (int parkingTypeId in update.ParkingTypeIds)
            {
                if (!await _parkingTypeRepository.CheckUniqueAsync(x => x.Id == parkingTypeId))
                {
                    await UpdatePopulateDropdowns(update);
                    return false;
                }
            }
            foreach (int roofTypeId in update.RoofTypeIds)
            {
                if (!await _roofTypeRepository.CheckUniqueAsync(x => x.Id == roofTypeId))
                {
                    await UpdatePopulateDropdowns(update);
                    return false;
                }
            }
            foreach (int viewTypeId in update.ViewTypeIds)
            {
                if (!await _viewTypeRepository.CheckUniqueAsync(x => x.Id == viewTypeId))
                {
                    await UpdatePopulateDropdowns(update);
                    return false;
                }
            }
            ICollection<ProductFeatures> featureToRemove = item.ProductFeatures
                .Where(ps => !update.FeatureIds.Contains(ps.FeaturesId)).ToList();
            foreach (var featureRemove in featureToRemove)
            {
                //item.ProductFeatures.Remove(featureRemove);
                _repository.DeleteFeatures(featureRemove);
            }

            ICollection<ProductFeatures> featureToAdd = update.FeatureIds
                .Except(item.ProductFeatures.Select(ps => ps.FeaturesId))
                .Select(featureId => new ProductFeatures { FeaturesId = featureId })
                .ToList();
            foreach (var featureAdd in featureToAdd)
            {
                item.ProductFeatures.Add(featureAdd);
            }

            ICollection<ProductExteriorType> exteriorTypeToRemove = item.ProductExteriorTypes
                .Where(ps => !update.ExteriorTypeIds.Contains(ps.ExteriorTypeId)).ToList();
            foreach (var exteriorTypeRemove in exteriorTypeToRemove)
            {
                //item.ProductExteriorTypes.Remove(exteriorTypeRemove);
                _repository.DeleteExteriorType(exteriorTypeRemove);
            }

            ICollection<ProductExteriorType> exteriorTypeToAdd = update.ExteriorTypeIds
                .Except(item.ProductExteriorTypes.Select(ps => ps.ExteriorTypeId))
                .Select(exteriorTypeId => new ProductExteriorType { ExteriorTypeId = exteriorTypeId })
                .ToList();
            foreach (var exteriorTypeAdd in exteriorTypeToAdd)
            {
                item.ProductExteriorTypes.Add(exteriorTypeAdd);
            }

            ICollection<ProductParkingType> parkingTypeToRemove = item.ProductParkingTypes
                .Where(ps => !update.ParkingTypeIds.Contains(ps.ParkingTypeId)).ToList();
            foreach (var parkingTypeRemove in parkingTypeToRemove)
            {
                //item.ProductParkingTypes.Remove(parkingTypeRemove);
                _repository.DeleteParkingType(parkingTypeRemove);
            }

            ICollection<ProductParkingType> parkingTypeToAdd = update.ParkingTypeIds
                .Except(item.ProductParkingTypes.Select(ps => ps.ParkingTypeId))
                .Select(parkingTypeId => new ProductParkingType { ParkingTypeId = parkingTypeId })
                .ToList();
            foreach (var parkingTypeAdd in parkingTypeToAdd)
            {
                item.ProductParkingTypes.Add(parkingTypeAdd);
            }

            ICollection<ProductRoofType> roofTypeToRemove = item.ProductRoofTypes
                    .Where(ps => !update.RoofTypeIds.Contains(ps.RoofTypeId)).ToList();
            foreach (var roofTyperemove in roofTypeToRemove)
            {
                //item.ProductRoofTypes.Remove(roofTyperemove);
                _repository.DeleteRoofType(roofTyperemove);
            }

            ICollection<ProductRoofType> roofTypeToAdd = update.RoofTypeIds
                .Except(item.ProductRoofTypes.Select(ps => ps.RoofTypeId))
                .Select(roofTypeId => new ProductRoofType { RoofTypeId = roofTypeId })
                .ToList();
            foreach (var roofTypeAdd in roofTypeToAdd)
            {
                item.ProductRoofTypes.Add(roofTypeAdd);
            }

            ICollection<ProductViewType> viewTypeToRemove = item.ProductViewTypes
                .Where(ps => !update.ViewTypeIds.Contains(ps.ViewTypeId)).ToList();
            foreach (var viewTypeRemove in viewTypeToRemove)
            {
                //item.ProductViewTypes.Remove(viewTypeRemove);
                _repository.DeleteViewType(viewTypeRemove);
            }

            ICollection<ProductViewType> viewTypeToAdd = update.ViewTypeIds
                .Except(item.ProductViewTypes.Select(ps => ps.ViewTypeId))
                .Select(viewTypeId => new ProductViewType { ViewTypeId = viewTypeId })
                .ToList();
            foreach (var viewTypeAdd in viewTypeToAdd)
            {
                item.ProductViewTypes.Add(viewTypeAdd);
            }

            if (update.MainPhoto != null)
            {
                if (!update.MainPhoto.ValidateType())
                {
                    await UpdatePopulateDropdowns(update);
                    model.AddModelError("MainPhoto", "File Not supported");
                    return false;
                }
                if (!update.MainPhoto.ValidataSize())
                {
                    await UpdatePopulateDropdowns(update);
                    model.AddModelError("MainPhoto", "Image should not be larger than 10 mb");
                    return false;
                }

                ProductImage main = item.ProductImages.FirstOrDefault(x => x.IsPrimary == true);

                await _cLoud.FileDeleteAsync(main.Url);
                //main.Url.DeleteFile(_env.WebRootPath, "assets", "images");
                item.ProductImages.Add(new ProductImage
                {
                    //CreatedBy = _http.HttpContext.User.Identity.Name,
                    IsPrimary = true,
                    Url = await _cLoud.FileCreateAsync(update.MainPhoto)
                    //Url = await update.MainPhoto.CreateFileAsync(_env.WebRootPath, "assets", "images")
                });
            }

            if (item.ProductImages == null) item.ProductImages = new List<ProductImage>();

            if (update.ImageIds == null) update.ImageIds = new List<int>();

            ICollection<ProductImage> remove = item.ProductImages
                    .Where(pi => pi.IsPrimary == null && !update.ImageIds.Exists(imgId => imgId == pi.Id)).ToList();

            foreach (var image in remove)
            {
                await _cLoud.FileDeleteAsync(image.Url);
                //image.Url.DeleteFile(_env.WebRootPath, "assets", "images");
                item.ProductImages.Remove(image);
            }

            tempData["Message"] = "";

            if (update.Photos != null)
            {
                foreach (var photo in update.Photos)
                {
                    if (!photo.ValidateType())
                    {
                        tempData["Message"] += $"<h5 class=\"text-danger\" style=\"margin-left: 100px; color: red;\">{photo.Name} type is not suitable</h5>";
                        continue;
                    }

                    if (!photo.ValidataSize(10))
                    {
                        tempData["Message"] += $"<h5 class=\"text-danger\" style=\"margin-left: 100px; color: red;\">{photo.Name} the size is not suitable</h5>";
                        continue;
                    }

                    item.ProductImages.Add(new ProductImage
                    {
                        //CreatedBy = _http.HttpContext.User.Identity.Name,
                        IsPrimary = null,
                        Url = await _cLoud.FileCreateAsync(photo)
                        //Url = await photo.CreateFileAsync(_env.WebRootPath, "assets", "images")
                    });
                }
            }

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateProductVM, Product>()
                    .ForMember(dest => dest.ProductImages, opt => opt.Ignore());
            });
            var mapper = config.CreateMapper();

            mapper.Map(update, item);
            await _repository.SaveChangeAsync();

            return true;
        }

        public async Task<UpdateProductVM> UpdateAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes ={
                $"{nameof(Product.Category)}",
                $"{nameof(Product.ProductFeatures)}.{nameof(ProductFeatures.Features)}",
                $"{nameof(Product.ProductExteriorTypes)}.{nameof(ProductExteriorType.ExteriorType)}",
                $"{nameof(Product.ProductParkingTypes)}.{nameof(ProductParkingType.ParkingType)}",
                $"{nameof(Product.ProductRoofTypes)}.{nameof(ProductRoofType.RoofType)}",
                $"{nameof(Product.ProductViewTypes)}.{nameof(ProductViewType.ViewType)}",
                $"{nameof(Product.ProductImages)}" };
            Product item = await _repository.GetByIdAsync(id, includes: includes);
            if (item == null) throw new NotFoundException("Your request was not found");

            UpdateProductVM update = _mapper.Map<UpdateProductVM>(item);

            update.FeatureIds = item.ProductFeatures.Select(p => p.FeaturesId).ToList();
            update.ExteriorTypeIds = item.ProductExteriorTypes.Select(p => p.ExteriorTypeId).ToList();
            update.ParkingTypeIds = item.ProductParkingTypes.Select(p => p.ParkingTypeId).ToList();
            update.RoofTypeIds = item.ProductRoofTypes.Select(p => p.RoofTypeId).ToList();
            update.ViewTypeIds = item.ProductViewTypes.Select(p => p.ViewTypeId).ToList();

            await UpdatePopulateDropdowns(update);

            return update;
        }
        public async Task<bool> CommentAsync(int productId, string comment, ITempDataDictionary tempData)
        {
            tempData["Comment"] = "";

            if (string.IsNullOrWhiteSpace(comment))
            {
                tempData["Comment"] += $"<p class=\"text-danger\" style=\"color: red;\">Comment is required</p>";
                return false;
            }
            if (comment.Length > 1500)
            {
                tempData["Comment"] += $"<p class=\"text-danger\" style=\"color: red;\">Comment max characters is 1-1500</p>";
                return false;
            }
            if (!Regex.IsMatch(comment, @"^[A-Za-z0-9\s,\.]+$"))
            {
                tempData["Comment"] += $"<p class=\"text-danger\" style=\" color: red;\">Comment can only contain letters, numbers, spaces, commas, and periods.</p>";
                return false;
            }
            ProductComment productComment = new ProductComment
            {
                Comment = comment,
                ProductId = productId,
                AppUserId = _http.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            await _repository.AddComment(productComment);
            await _repository.SaveChangeAsync();
            return true;
        }
        public async Task<bool> ReplyAsync(int productCommnetId, string comment, ITempDataDictionary tempData)
        {
            tempData["Reply"] = "";

            if (string.IsNullOrWhiteSpace(comment))
            {
                tempData["Reply"] += $"<p class=\"text-danger\" style=\"color: red;\">Comment is required</p>";
                return false;
            }
            if (comment.Length > 1500)
            {
                tempData["Reply"] += $"<p class=\"text-danger\" style=\"color: red;\">Comment max characters is 1-1500</p>";
                return false;
            }
            if (!Regex.IsMatch(comment, @"^[A-Za-z0-9\s,\.]+$"))
            {
                tempData["Reply"] += $"<p class=\"text-danger\" style=\" color: red;\">Comment can only contain letters, numbers, spaces, commas, and periods.</p>";
                return false;
            }
            ProductReply productComment = new ProductReply
            {
                ReplyComment = comment,
                ProductCommentId = productCommnetId,
                AppUserId = _http.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            await _repository.AddReply(productComment);
            await _repository.SaveChangeAsync();
            return true;
        }
        public async Task<bool> AgentMessage(int productId, string message, ITempDataDictionary tempData)
        {
            tempData["AgentMessage"] = "";

            if (string.IsNullOrWhiteSpace(message))
            {
                tempData["AgentMessage"] += $"<p class=\"text-danger\" style=\"color: red;\">Message is required</p>";
                return false;
            }
            if (message.Length > 1500)
            {
                tempData["AgentMessage"] += $"<p class=\"text-danger\" style=\"color: red;\">Message max characters is 1-1500</p>";
                return false;
            }
            if (!Regex.IsMatch(message, @"^[A-Za-z0-9\s,\.]+$"))
            {
                tempData["AgentMessage"] += $"<p class=\"text-danger\" style=\" color: red;\">Message can only contain letters, numbers, spaces, commas, and periods.</p>";
                return false;
            }

            if (productId <= 0) throw new WrongRequestException("The request sent does not exist");
            Product item = await _repository.GetByIdAsync(productId, false,$"{nameof(Product.AppUser)}");
            if (item == null) throw new NotFoundException("Your request was not found");
            AppUser user = await _userManager.FindByIdAsync(_http.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user == null) throw new NotFoundException("Your request was not found");
            await _email.SendMailAsync(item.AppUser.Email, $"{user.Name} {user.Surname} sent a message from the {item.Name} product", 
                $"{message}");
            return true;
        }
    }
}

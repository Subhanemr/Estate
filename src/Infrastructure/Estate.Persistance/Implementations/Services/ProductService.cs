using AutoMapper;
using Estate.Application.Abstractions.Repositories;
using Estate.Application.Abstractions.Services;
using Estate.Application.ViewModels.Category;
using Estate.Application.ViewModels.ExteriorType;
using Estate.Application.ViewModels.Features;
using Estate.Application.ViewModels.ParkingType;
using Estate.Application.ViewModels.Product;
using Estate.Application.ViewModels.RoofType;
using Estate.Application.ViewModels.ViewType;
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

namespace Estate.Persistance.Implementations.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IFeaturesRepository _featuresRepository;
        private readonly IExteriorTypeRepository _exteriorTypeRepository;
        private readonly IParkingTypeRepository _parkingTypeRepository;
        private readonly IRoofTypeRepository _roofTypeRepository;
        private readonly IViewTypeRepository _viewTypeRepository;
        private readonly IHttpContextAccessor _http;
        private readonly IWebHostEnvironment _env;
        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;
        private readonly UserManager<AppUser> _userManager;

        public ProductService(IMapper mapper, IProductRepository repository,
            ICategoryRepository categoryRepository, IFeaturesRepository featuresRepository, IExteriorTypeRepository exteriorTypeRepository,
            IParkingTypeRepository parkingTypeRepository, IRoofTypeRepository roofTypeRepository, IViewTypeRepository viewTypeRepository,
            IHttpContextAccessor http, IWebHostEnvironment env, ITempDataDictionaryFactory tempDataDictionaryFactory, UserManager<AppUser> userManager)
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
            _tempDataDictionaryFactory = tempDataDictionaryFactory;
            _userManager = userManager;
        }

        public async void CreatePopulateDropdowns(CreateProductVM create)
        {
            create.Categories = _mapper.Map<ICollection<IncludeCategoryVM>>(await _categoryRepository.GetAll().ToListAsync());
            create.Features = _mapper.Map<ICollection<IncludeFeaturesVM>>(await _featuresRepository.GetAll().ToListAsync());
            create.ExteriorTypes = _mapper.Map<ICollection<IncludeExteriorTypeVM>>(await _exteriorTypeRepository.GetAll().ToListAsync());
            create.ParkingTypes = _mapper.Map<ICollection<IncludeParkingTypeVM>>(await _parkingTypeRepository.GetAll().ToListAsync());
            create.RoofTypes = _mapper.Map<ICollection<IncludeRoofTypeVM>>(await _roofTypeRepository.GetAll().ToListAsync());
            create.ViewTypes = _mapper.Map<ICollection<IncludeViewTypeVM>>(await _viewTypeRepository.GetAll().ToListAsync());
        }
        public async void UpdatePopulateDropdowns(UpdateProductVM update)
        {
            update.Categories = _mapper.Map<ICollection<IncludeCategoryVM>>(await _categoryRepository.GetAll().ToListAsync());
            update.Features = _mapper.Map<ICollection<IncludeFeaturesVM>>(await _featuresRepository.GetAll().ToListAsync());
            update.ExteriorTypes = _mapper.Map<ICollection<IncludeExteriorTypeVM>>(await _exteriorTypeRepository.GetAll().ToListAsync());
            update.ParkingTypes = _mapper.Map<ICollection<IncludeParkingTypeVM>>(await _parkingTypeRepository.GetAll().ToListAsync());
            update.RoofTypes = _mapper.Map<ICollection<IncludeRoofTypeVM>>(await _roofTypeRepository.GetAll().ToListAsync());
            update.ViewTypes = _mapper.Map<ICollection<IncludeViewTypeVM>>(await _viewTypeRepository.GetAll().ToListAsync());
        }

        public async Task<bool> CreateAsync(CreateProductVM create, ModelStateDictionary model)
        {
            if (!model.IsValid)
            {
                CreatePopulateDropdowns(create);
                return false;
            }
            AppUser user = await _userManager.FindByNameAsync(_http.HttpContext.User.Identity.Name);

            if (!await _categoryRepository.CheckUniqueAsync(x => x.Id == create.CategoryId))
            {
                CreatePopulateDropdowns(create);
                return false;
            }
            foreach (int featureId in create.FeatureIds)
            {
                if (!await _featuresRepository.CheckUniqueAsync(x => x.Id == featureId))
                {
                    CreatePopulateDropdowns(create);
                    return false;
                }
            }
            foreach (int exteriorTypeId in create.ExteriorTypeIds)
            {
                if (!await _exteriorTypeRepository.CheckUniqueAsync(x => x.Id == exteriorTypeId))
                {
                    CreatePopulateDropdowns(create);
                    return false;
                }
            }
            foreach (int parkingTypeId in create.ParkingTypeIds)
            {
                if (!await _parkingTypeRepository.CheckUniqueAsync(x => x.Id == parkingTypeId))
                {
                    CreatePopulateDropdowns(create);
                    return false;
                }
            }
            foreach (int roofTypeId in create.RoofTypeIds)
            {
                if (!await _roofTypeRepository.CheckUniqueAsync(x => x.Id == roofTypeId))
                {
                    CreatePopulateDropdowns(create);
                    return false;
                }
            }
            foreach (int viewTypeId in create.ViewTypeIds)
            {
                if (!await _viewTypeRepository.CheckUniqueAsync(x => x.Id == viewTypeId))
                {
                    CreatePopulateDropdowns(create);
                    return false;
                }
            }
            if (!create.MainPhoto.ValidateType())
            {
                CreatePopulateDropdowns(create);
                model.AddModelError("MainPhoto", "File Not supported");
                return false;
            }
            if (!create.MainPhoto.ValidataSize())
            {
                CreatePopulateDropdowns(create);
                model.AddModelError("MainPhoto", "Image should not be larger than 10 mb");
                return false;
            }
            ProductImage mainImage = new ProductImage
            {
                CreatedBy = user.UserName,
                IsPrimary = true,
                Url = await create.MainPhoto.CreateFileAsync(_env.WebRootPath, "assets", "images")
            };

            Product item = _mapper.Map<Product>(create);

            item.ProductImages = new List<ProductImage> { mainImage };

            if (item.ProductImages == null) item.ProductImages = new List<ProductImage>();

            var httpContext = _http.HttpContext;
            var tempData = _tempDataDictionaryFactory.GetTempData(httpContext);

            tempData["Message"] = "";

            foreach (IFormFile photo in create.Photos)
            {
                if (!photo.ValidateType())
                {
                    tempData["Message"] += $"<p class=\"text-danger\">{photo.Name} type is not suitable</p>";
                    continue;
                }

                if (!photo.ValidataSize(10))
                {
                    tempData["Message"] += $"<p class=\"text-danger\">{photo.Name} the size is not suitable</p>";
                    continue;
                }

                item.ProductImages.Add(new ProductImage
                {
                    CreatedBy = user.UserName,
                    IsPrimary = null,
                    Url = await photo.CreateFileAsync(_env.WebRootPath, "assets", "images")
                });
            }
            item.CreatedBy = user.UserName;

            await _repository.AddAsync(item);
            await _repository.SaveChanceAsync();

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
                image.Url.DeleteFile(_env.WebRootPath, "assets", "images");
            }
            _repository.Delete(item);
            await _repository.SaveChanceAsync();
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

        public async Task<ICollection<ItemProductVM>> GetAllWhereByOrderAsync(int take, Expression<Func<Product, object>>? orderExpression, int page = 1)
        {
            string[] includes ={
                $"{nameof(Product.Category)}",
                $"{nameof(Product.ProductImages)}" };
            ICollection<Product> items = await _repository
                    .GetAllWhereByOrder(orderException: orderExpression, skip: (page - 1) * take, take: take, IsTracking: false, includes: includes).ToListAsync();

            ICollection<ItemProductVM> vMs = _mapper.Map<ICollection<ItemProductVM>>(items);

            return vMs;
        }

        public async Task<GetProductVM> GetByIdAsync(int id, int take, int page = 1)
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

            GetProductVM get = _mapper.Map<GetProductVM>(item);

            return get;
        }

        public async Task ReverseSoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Product item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            item.IsDeleted = false;
            await _repository.SaveChanceAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Product item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            item.IsDeleted = true;
            await _repository.SaveChanceAsync();
        }

        public async Task<bool> UpdatePostAsync(int id, UpdateProductVM update, ModelStateDictionary model)
        {
            if (!model.IsValid)
            {
                UpdatePopulateDropdowns(update);
                return false;
            }
            AppUser user = await _userManager.FindByNameAsync(_http.HttpContext.User.Identity.Name);

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

            if (!await _categoryRepository.CheckUniqueAsync(x => x.Id == update.CategoryId))
            {
                UpdatePopulateDropdowns(update);
                return false;
            }
            foreach (int featureId in update.FeatureIds)
            {
                if (!await _featuresRepository.CheckUniqueAsync(x => x.Id == featureId))
                {
                    UpdatePopulateDropdowns(update);
                    return false;
                }
            }
            foreach (int exteriorTypeId in update.ExteriorTypeIds)
            {
                if (!await _exteriorTypeRepository.CheckUniqueAsync(x => x.Id == exteriorTypeId))
                {
                    UpdatePopulateDropdowns(update);
                    return false;
                }
            }
            foreach (int parkingTypeId in update.ParkingTypeIds)
            {
                if (!await _parkingTypeRepository.CheckUniqueAsync(x => x.Id == parkingTypeId))
                {
                    UpdatePopulateDropdowns(update);
                    return false;
                }
            }
            foreach (int roofTypeId in update.RoofTypeIds)
            {
                if (!await _roofTypeRepository.CheckUniqueAsync(x => x.Id == roofTypeId))
                {
                    UpdatePopulateDropdowns(update);
                    return false;
                }
            }
            foreach (int viewTypeId in update.ViewTypeIds)
            {
                if (!await _viewTypeRepository.CheckUniqueAsync(x => x.Id == viewTypeId))
                {
                    UpdatePopulateDropdowns(update);
                    return false;
                }
            }
            ICollection<ProductFeatures> featureToRemove = item.ProductFeatures
                .Where(ps => !update.FeatureIds.Contains(ps.FeaturesId)).ToList();
            foreach (var featureRemove in featureToRemove)
            {
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
                    UpdatePopulateDropdowns(update);
                    model.AddModelError("MainPhoto", "File Not supported");
                    return false;
                }
                if (!update.MainPhoto.ValidataSize())
                {
                    UpdatePopulateDropdowns(update);
                    model.AddModelError("MainPhoto", "Image should not be larger than 10 mb");
                    return false;
                }
                ProductImage main = item.ProductImages.FirstOrDefault(x => x.IsPrimary == true);
                main.Url.DeleteFile(_env.WebRootPath, "assets", "images");
                item.ProductImages.Add(new ProductImage
                {
                    CreatedBy = user.UserName,
                    IsPrimary = true,
                    Url = await update.MainPhoto.CreateFileAsync(_env.WebRootPath, "assets", "images")
                });
            }

            if (item.ProductImages == null) item.ProductImages = new List<ProductImage>();

            if (update.ImageIds == null) update.ImageIds = new List<int>();

            ICollection<ProductImage> remove = item.ProductImages
                    .Where(pi => pi.IsPrimary == null && !update.ImageIds.Exists(imgId => imgId == pi.Id)).ToList();

            foreach (var image in remove)
            {
                image.Url.DeleteFile(_env.WebRootPath, "assets", "images");
                item.ProductImages.Remove(image);
            }

            var httpContext = _http.HttpContext;
            var tempData = _tempDataDictionaryFactory.GetTempData(httpContext);

            tempData["Message"] = "";

            if (update.Photos != null)
            {
                foreach (var photo in update.Photos)
                {
                    if (!photo.ValidateType())
                    {
                        tempData["Message"] += $"<p class=\"text-danger\">{photo.Name} type is not suitable</p>";
                        continue;
                    }

                    if (!photo.ValidataSize(10))
                    {
                        tempData["Message"] += $"<p class=\"text-danger\">{photo.Name} the size is not suitable</p>";
                        continue;
                    }

                    item.ProductImages.Add(new ProductImage
                    {
                        CreatedBy = user.UserName,
                        IsPrimary = null,
                        Url = await photo.CreateFileAsync(_env.WebRootPath, "assets", "images")
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
            await _repository.SaveChanceAsync();

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
            UpdatePopulateDropdowns(update);

            return update;
        }
    }
}

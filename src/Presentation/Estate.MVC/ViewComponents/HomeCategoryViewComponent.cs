using Estate.Application.Abstractions.Services;
using Estate.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Estate.MVC.ViewComponents
{
    public class HomeCategoryViewComponent : ViewComponent
    {
        private readonly ICategoryService _service;

        public HomeCategoryViewComponent(ICategoryService service)
        {
            _service = service;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ICollection<ItemCategoryVM> items = await _service.GetAllWhereByOrderAsync(3, x => x.Products.Count, 1);
            return View(items);
        }
    }
}

using Estate.Application.Abstractions.Services;
using Estate.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Estate.MVC.ViewComponents
{
    public class DetailProductsViewComponent : ViewComponent
    {
        private readonly IProductService _service;

        public DetailProductsViewComponent(IProductService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync(GetProductVM product)
        {
            ICollection<ItemProductVM> items = await _service.GetAllWhereByBoolAsync(3, x => x.Id != product.Id && x.CategoryId == product.CategoryId, 1);
            return View(items);
        }
    }
}

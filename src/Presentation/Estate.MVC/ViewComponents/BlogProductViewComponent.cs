using AutoMapper;
using Estate.Application.Abstractions.Services;
using Estate.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Estate.MVC.ViewComponents
{
    public class BlogProductViewComponent : ViewComponent
    {
        private readonly IProductService _service;

        public BlogProductViewComponent(IProductService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ICollection<ItemProductVM> items = await _service.GetAllWhereByOrderAsync(3, x => x.ProductComments.Count, 1);
            return View(items);
        }
    }
}

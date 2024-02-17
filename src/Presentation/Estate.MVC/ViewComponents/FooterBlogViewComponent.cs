using Estate.Application.Abstractions.Services;
using Estate.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Estate.MVC.ViewComponents
{
    public class FooterBlogViewComponent : ViewComponent
    {
        private readonly IBlogService _service;

        public FooterBlogViewComponent(IBlogService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ICollection<ItemBlogVM> items = await _service.GetAllWhereByOrderAsync(3, 1, x => x.BlogComments.Count);
            return View(items);
        }
    }
}

using Estate.Application.Abstractions.Services;
using Estate.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Estate.MVC.ViewComponents
{
    public class BlogsViewComponent : ViewComponent
    {
        private readonly IBlogService _service;

        public BlogsViewComponent(IBlogService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            ICollection<ItemBlogVM> items = await _service.GetAllWhereByOrderAsync(3, x => x.Id != id, x => x.BlogComments.Count, 1);
            return View(items);
        }
    }
}

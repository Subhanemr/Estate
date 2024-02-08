using Microsoft.AspNetCore.Mvc;

namespace Estate.MVC.ViewComponents
{
    public class BlogsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}

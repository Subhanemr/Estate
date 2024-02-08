using Microsoft.AspNetCore.Mvc;

namespace Estate.MVC.ViewComponents
{
    public class BlogProductViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Estate.MVC.ViewComponents
{
    public class LoginViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}

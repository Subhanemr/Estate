using Estate.Application.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace Estate.MVC.ViewComponents
{
    public class RegisterViewComponent : ViewComponent
    {
        private readonly IAccountService _service;

        public RegisterViewComponent(IAccountService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }

    }
}

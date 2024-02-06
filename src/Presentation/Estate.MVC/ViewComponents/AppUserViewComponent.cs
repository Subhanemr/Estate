using Estate.Domain.Entities;
using Estate.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Estate.MVC.ViewComponents
{
    public class AppUserViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor _http;
        private readonly UserManager<AppUser> _userManager;

        public AppUserViewComponent(IHttpContextAccessor http, UserManager<AppUser> userManager)
        {
            _http = http;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            AppUser user = await _userManager.FindByNameAsync(_http.HttpContext.User.Identity.Name);
            if (user == null) throw new NotFoundException("Your request was not found");

            return View(user);
        }
    }
}

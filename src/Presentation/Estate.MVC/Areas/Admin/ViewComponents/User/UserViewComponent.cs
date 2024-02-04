using Estate.Domain.Entities;
using Estate.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Estate.MVC.Areas.Admin.ViewComponents.User
{ 
    public class UserViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor _http;
        private readonly UserManager<AppUser> _userManager;

        public UserViewComponent(IHttpContextAccessor http, UserManager<AppUser> userManager)
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

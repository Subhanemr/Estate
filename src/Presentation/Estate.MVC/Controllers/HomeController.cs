using Estate.Application.Abstractions.Services;
using Estate.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Estate.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;

        public HomeController(IProductService productService, IUserService userService)
        {
            _productService = productService;
            _userService = userService;
        }

        public async Task<IActionResult> Index(string? returnUrl)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }
            HomeVM home = new HomeVM
            {
                Pagination = await _productService.GetAllWhereByOrderFilterAsync(6, 1, x => x.ProductComments.Count),
                Agents = await _userService.GetAllWhereByOrderAsync(6)
            };
            return View(home);
        }

        public IActionResult ErrorPage(string error)
        {
            if (error == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model: error);
        }
        public IActionResult TermsService()
        {
            return View();
        }
        public IActionResult FAQ()
        {
            return View();
        }
    }
}

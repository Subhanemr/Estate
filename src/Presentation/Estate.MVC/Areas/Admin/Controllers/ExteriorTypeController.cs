using Estate.Application.Abstractions.Services;
using Estate.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Estate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ExteriorTypeController : Controller
    {
        private readonly IExteriorTypeService _service;

        public ExteriorTypeController(IExteriorTypeService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateExteriorTypeVM create)
        {
            bool result = await _service.CreateAsync(create, ModelState);
            if (!result)
            {
                return View(create);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

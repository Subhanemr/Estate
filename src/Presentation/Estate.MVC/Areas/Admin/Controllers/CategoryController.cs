﻿using Estate.Application.Abstractions.Services;
using Estate.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Estate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string? search, int order = 1, int page = 1)
        {
            return View(model: await _service.GetFilteredAsync(search, 10, page, order));
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryVM create)
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

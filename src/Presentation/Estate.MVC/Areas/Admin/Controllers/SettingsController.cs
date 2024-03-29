﻿using Estate.Application.Abstractions.Repositories;
using Estate.Application.Abstractions.Services;
using Estate.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Moderator")]
    [AutoValidateAntiforgeryToken]
    public class SettingsController : Controller
    {
        private readonly ISettingsService _service;

        public SettingsController(ISettingsService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string? search, string? returnUrl, int order = 1, int page = 1)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return View(model: await _service.GetFilteredAsync(search, 10, page, order));
        }

        public async Task<IActionResult> Update(int id, string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(await _service.UpdateAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, string? returnUrl, UpdateSettingsVM update)
        {
            bool result = await _service.UpdatePostAsync(id, update, ModelState);
            if (!result)
            {
                return View(update);
            }
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

﻿using Estate.Application.Abstractions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Moderator")]
    [AutoValidateAntiforgeryToken]
    public class UserController : Controller
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string? search, int order = 1, int page = 1)
        {
            return View(model: await _service.GetFilteredAsync(search, 10, page, order));
        }

        public async Task<IActionResult> DeletedItems(string? search, int order = 1, int page = 1)
        {
            return View(model: await _service.GetFilteredAsync(search, 10, page, order, true));
        }

        public async Task<IActionResult> Detail(string id)
        {
            return View(await _service.GetByIdAsync(id));
        }

        public async Task<IActionResult> SoftDelete(string id)
        {
            await _service.SoftDeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ReverseSoftDelete(string id)
        {
            await _service.ReverseSoftDeleteAsync(id);

            return RedirectToAction(nameof(DeletedItems));
        }

        public async Task<IActionResult> IsSoulOfAgencyAsync(string id)
        {
            await _service.IsSoulOfAgencyAsync(id);

            return RedirectToAction(nameof(DeletedItems));
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(DeletedItems));
        }
        public async Task<IActionResult> GiveRoleModerator(string id)
        {
            await _service.GiveRoleModeratorAsync(id);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteRoleModeratorAsync(string id)
        {
            await _service.DeleteRoleModeratorAsync(id);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteRoleAgentAsync(string id)
        {
            await _service.DeleteRoleAgentAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}

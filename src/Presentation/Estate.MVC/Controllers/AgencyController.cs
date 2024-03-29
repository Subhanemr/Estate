﻿using Estate.Application.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace Estate.MVC.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AgencyController : Controller
    {
        private readonly IAgencyService _service;
        private readonly IUserService _userService;

        public AgencyController(IAgencyService service, IUserService userService)
        {
            _service = service;
            _userService = userService;
        }

        public async Task<IActionResult> Detail(int id)
        {
            return View(await _service.GetByIdAsync(id));
        }
        public async Task<IActionResult> Agent(string id)
        {
            return View(await _userService.GetByIdAsync(id));
        }
        public async Task<IActionResult> AgentN(string name)
        {
            return View(await _userService.GetByUserNameAsync(name));
        }
        public async Task<IActionResult> AgentMessage(string agentId, string message)
        {
            await _userService.AgentMessage(agentId, message, TempData);
            //return PartialView("AgentMessage/_AgentMessagePartialView", await _service.GetByIdAsync(agentId));

            return RedirectToAction("Agent", "Agency", new { Id = agentId });
        }
    }
}

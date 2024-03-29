﻿using Estate.Application.Abstractions.Services;
using Estate.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Estate.MVC.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login, string? returnUrl)
        {
            bool result = await _service.LogInAsync(login, ModelState);
            if (!result)
            {
                return View(login);
            }
            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            bool result = await _service.RegisterAsync(register, ModelState, Url);
            if (!result)
            {
                return View(register);
            }
            return RedirectToAction(nameof(SuccessfullyRegistred));
        }
        public async Task<IActionResult> LogOut()
        {
            await _service.LogOutAsync();

            return RedirectToAction("Index", "Home", new { Area = "" });
        }
        public IActionResult SuccessfullyRegistred()
        {
            return View();
        }
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            bool result = await _service.ConfirmEmail(token, email);
            if (!result)
            {
                return View();
            }
            return View();
        }
        public IActionResult ForgotPasswordSended()
        {
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(FindAccountVM account)
        {
            bool result = await _service.ForgotPassword(account, ModelState, Url);
            if (!result)
            {
                return View(account);
            }
            return RedirectToAction(nameof(ForgotPasswordSended));
        }
        public IActionResult ResetPassword(string id, string token)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(string id,string token, ResetPasswordVM resetPassword)
        {
            bool result = await _service.ResetPassword(id, token, resetPassword, ModelState);
            if (!result)
            {
                return View(resetPassword);
            }
            return RedirectToAction(nameof(Login));
        }
    }
}

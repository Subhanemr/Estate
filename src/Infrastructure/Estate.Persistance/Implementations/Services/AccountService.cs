﻿using AutoMapper;
using Estate.Application.Abstractions.Services;
using Estate.Application.ViewModels;
using Estate.Domain.Entities;
using Estate.Domain.Enums;
using Estate.Infrastructure.Exceptions;
using Estate.Infrastructure.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Estate.Persistance.Implementations.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _http;

        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            IMapper mapper, IEmailService emailService, IHttpContextAccessor http)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _emailService = emailService;
            _http = http;
        }

        public async Task<bool> LogInAsync(LoginVM login, ModelStateDictionary model)
        {
            if (!model.IsValid) return false;
            AppUser user = await _userManager.FindByNameAsync(login.UserNameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(login.UserNameOrEmail);
                if (user == null)
                {
                    model.AddModelError(string.Empty, "Username, Email or Password is wrong");
                    return false;
                }
            }
            if (user.IsActivate == true)
            {
                model.AddModelError(string.Empty, "Your account is not active");
                return false;
            }
            var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.IsRemembered, true);
            if (result.IsLockedOut)
            {
                model.AddModelError(string.Empty, "Your Account is locked-out please wait");
                return false;
            }
            if (!result.Succeeded)
            {
                model.AddModelError(string.Empty, "Username, Email or Password is wrong");
                return false;
            }

            _http.HttpContext.Response.Cookies.Delete("FavoriteEstate");

            return true;
        }
        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<bool> RegisterAsync(RegisterVM register, ModelStateDictionary model, IUrlHelper url)
        {
            if (!model.IsValid) return false;

            AppUser user = _mapper.Map<AppUser>(register);
            user.Name = user.Name.Capitalize();
            user.Surname = user.Surname.Capitalize();

            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    model.AddModelError(string.Empty, error.Description);
                }
                return false;
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = url.Action("ConfirmEmail", "Account", new { token, Email = user.Email }, _http.HttpContext.Request.Scheme);
            await _emailService.SendMailAsync(user.Email, "Email Confirmation", $"<a href=\"{confirmationLink}\" style=\"background-color: #4CAF50; /* Green */\r\n  border: none;\r\n  color: white;\r\n  padding: 15px 32px;\r\n  text-align: center;\r\n  text-decoration: none;\r\n  display: inline-block;\r\n  font-size: 16px;\r\n  margin: 4px 2px;\r\n  cursor: pointer;\r\n  border-radius: 10px;\" >Verify Email</a>\r\n", true);

            await _userManager.AddToRoleAsync(user, UserRoles.Member.ToString());

            return true;
        }
        public async Task<bool> ConfirmEmail(string token, string email)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(email);
            if (appUser == null) throw new NotFoundException("Your request was not found");
            var result = await _userManager.ConfirmEmailAsync(appUser, token);
            if (!result.Succeeded)
            {
                string errors = "";
                foreach (var error in result.Errors)
                {
                    errors += error.Description;
                }
                throw new WrongRequestException(errors);
            }

            _http.HttpContext.Response.Cookies.Delete("FavoriteEstate");
            await _signInManager.SignInAsync(appUser, false);

            return true;
        }

        public async Task<bool> ForgotPassword(FindAccountVM account, ModelStateDictionary model, IUrlHelper url)
        {
            if (string.IsNullOrWhiteSpace(account.UserNameOrEmail))
            {
                model.AddModelError(string.Empty, "Username, Email or Password is wrong");
                return false;

            }
            AppUser user = await _userManager.FindByNameAsync(account.UserNameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(account.UserNameOrEmail);
                if (user == null)
                {
                    model.AddModelError(string.Empty, "Username or Email is wrong");
                    return false;
                }
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var confirmationLink = url.Action("ResetPassword", "Account", new { Id = user.Id, Token = token }, _http.HttpContext.Request.Scheme);
            await _emailService.SendMailAsync(user.Email, "Reset Password", $"<a href=\"{confirmationLink}\" style=\"background-color: #4CAF50; /* Green */\r\n  border: none;\r\n  color: white;\r\n  padding: 15px 32px;\r\n  text-align: center;\r\n  text-decoration: none;\r\n  display: inline-block;\r\n  font-size: 16px;\r\n  margin: 4px 2px;\r\n  cursor: pointer;\r\n  border-radius: 10px;\" >Reset Password</a>\r\n", true);

            return true;
        }

        public async Task<bool> ResetPassword(string id, string token, ResetPasswordVM resetPassword, ModelStateDictionary model)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(token)) throw new NotFoundException("Your request was not found");
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                if (user == null) throw new NotFoundException("Your request was not found");
            }

            var result = await _userManager.ResetPasswordAsync(user, token, resetPassword.NewPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    model.AddModelError(string.Empty, error.Description);
                }
                return false;
            }
            _http.HttpContext.Response.Cookies.Delete("FavoriteEstate");

            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(user, false);
            return true;
        }
    }
}

using AutoMapper;
using Estate.Application.Abstractions.Services;
using Estate.Application.ViewModels;
using Estate.Domain.Entities;
using Estate.Domain.Enums;
using Estate.Infrastructure.Exceptions;
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
            if(user.IsActivate == true)
            {
                model.AddModelError("Error", "Your account is not active");
                return false;
            }
            var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.IsRemembered, true);
            if (result.IsLockedOut)
            {
                model.AddModelError("Error", "Your Account is locked-out please wait");
                return false;
            }
            if (!result.Succeeded)
            {
                model.AddModelError("Error", "Username, Email or Password is wrong");
                return false;
            }
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
            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    model.AddModelError("Error", error.Description);
                }
                return false;
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = url.Action("ConfirmEmail", "Account", new { token, Email = user.Email }, _http.HttpContext.Request.Scheme);
            await _emailService.SendMailAsync(user.Email, "Email Confirmation", confirmationLink);

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
                throw new WrongRequestException("The request sent does not exist");
            }
            await _signInManager.SignInAsync(appUser, false);

            return true;
        }
    }
}

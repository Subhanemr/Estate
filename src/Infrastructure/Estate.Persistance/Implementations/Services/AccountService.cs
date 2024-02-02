using AutoMapper;
using Estate.Application.Abstractions.Services;
using Estate.Application.ViewModels.Account;
using Estate.Domain.Entities;
using Estate.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Estate.Persistance.Implementations.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
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
            return true;
        }

        public async Task<bool> RegisterAsync(RegisterVM register, ModelStateDictionary model)
        {
            if (!model.IsValid) return false;
            AppUser user = _mapper.Map<AppUser>(register);
            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    model.AddModelError(string.Empty, error.Description);
                }
                return false;
            }
            await _userManager.AddToRoleAsync(user, UserRoles.Member.ToString());

            return true;
        }
    }
}

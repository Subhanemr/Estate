﻿using Estate.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Estate.Application.Abstractions.Services
{
    public interface IAccountService
    {
        Task<bool> RegisterAsync(RegisterVM register, ModelStateDictionary model, IUrlHelper url);
        Task<bool> LogInAsync(LoginVM login, ModelStateDictionary model);
        Task LogOutAsync();
        Task<bool> ConfirmEmail(string token, string email);
        Task<bool> ForgotPassword(FindAccountVM account, ModelStateDictionary model, IUrlHelper url);
        Task<bool> ResetPassword(string id, string token, ResetPasswordVM resetPassword, ModelStateDictionary model);
    }
}

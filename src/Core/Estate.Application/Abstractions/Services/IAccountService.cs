using Estate.Application.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Estate.Application.Abstractions.Services
{
    public interface IAccountService
    {
        Task<bool> RegisterAsync(RegisterVM register, ModelStateDictionary model);
        Task<bool> LogInAsync(LoginVM login, ModelStateDictionary model);
    }
}

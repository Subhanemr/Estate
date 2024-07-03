using Estate.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Estate.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<PaginationVM<ItemAppUserVM>> GetFilteredAsync(string? search, int take, int page, int order, bool isDeleted = false);
        Task<ICollection<ItemAppUserVM>> GetAllWhereByOrderAsync(int take);
        Task<GetAppUserVM> GetByIdAdminAsync(string id);
        Task<GetAppUserVM> GetByIdAsync(string id);
        Task<GetAppUserVM> GetByUserNameAdminAsync(string userName);
        Task<GetAppUserVM> GetByUserNameAsync(string userName);
        Task ReverseSoftDeleteAsync(string id);
        Task SoftDeleteAsync(string id);
        Task DeleteAsync(string id);
        Task GiveRoleModeratorAsync(string id);
        Task DeleteRoleModeratorAsync(string id);
        Task DeleteRoleAgentAsync(string id);
        Task IsSoulOfAgencyAsync(string id);
        Task<EditUserVM> EditUser(string id);
        Task<bool> EditUserAsync(string id, EditUserVM update, ModelStateDictionary model);
        Task ForgotPassword(string id, IUrlHelper url);
        Task<bool> ChangePassword(string id, string token, ChangePasswordVM fogotPassword, ModelStateDictionary model);
        Task<CreateAppUserAgentVM> BeAAgent(string id);
        Task<bool> BeAAgentPost(string id, CreateAppUserAgentVM create, ModelStateDictionary model, ITempDataDictionary tempData);
        Task<UpdateAppUserAgentVM> UpdateAgentAsync(string id);
        Task<bool> UpdateAgentPostAsync(string id, UpdateAppUserAgentVM update, ModelStateDictionary model, ITempDataDictionary tempData);
        Task<bool> AgentMessage(string agentId, string message, ITempDataDictionary tempData);
    }
}

using Microsoft.AspNetCore.Http;

namespace Estate.Application.Abstractions.Services
{
    public interface ICLoudService
    {
        Task<string> FileCreateAsync(IFormFile file);
        Task<bool> FileDeleteAsync(string filename);

    }
}

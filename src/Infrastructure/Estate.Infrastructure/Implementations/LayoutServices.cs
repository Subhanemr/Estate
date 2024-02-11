using Estate.Application.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Estate.Infrastructure.Implementations
{
    public class LayoutServices
    {
        private readonly ISettingsRepository _repository;

        public LayoutServices(ISettingsRepository repository)
        {
            _repository = repository;
        }

        public async Task<Dictionary<string, string>> GetSettingsAsync()
        {
            Dictionary<string, string> keyValuePairs = await _repository.GetAll(false).ToDictionaryAsync(p => p.Key, p => p.Value);
            return keyValuePairs;
        }
    }
}

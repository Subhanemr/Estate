using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Estate.Application.Abstractions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Estate.Infrastructure.Implementations
{
    public class CloudService : ICLoudService
    {
        private readonly IConfiguration _configuration;

        public CloudService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> FileCreateAsync(IFormFile file)
        {
            string fileName = string.Concat(Guid.NewGuid(), file.FileName.Substring(file.FileName.LastIndexOf('.')));
            var myAccount = new Account { ApiKey = _configuration["CloudinarySettings:APIKey"], ApiSecret = _configuration["CloudinarySettings:APISecret"], Cloud = _configuration["CloudinarySettings:CloudName"] };

            Cloudinary _cloudinary = new Cloudinary(myAccount);
            _cloudinary.Api.Secure = true;
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(fileName, stream),
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            string url = uploadResult.SecureUri.ToString();
            return url;
        }
        public async Task<bool> FileDeleteAsync(string filename)
        {
            var myAccount = new Account
            {
                ApiKey = _configuration["CloudinarySettings:APIKey"],
                ApiSecret = _configuration["CloudinarySettings:APISecret"],
                Cloud = _configuration["CloudinarySettings:CloudName"]
            };

            Cloudinary _cloudinary = new Cloudinary(myAccount);
            _cloudinary.Api.Secure = true;

            var deletionParams = new DeletionParams(filename);
            if (filename.Contains("https://res.cloudinary.com/dzsysx73x/image/upload/v1707148401/cwlzdvof54s1fw1eo19z.png"))
            {
                return true;
            }
            var deletionResult = await _cloudinary.DestroyAsync(deletionParams);

            return deletionResult.Result == "ok";
        }
    }
}

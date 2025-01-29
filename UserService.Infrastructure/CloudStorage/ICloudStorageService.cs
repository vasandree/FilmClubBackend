using Microsoft.AspNetCore.Http;

namespace UserService.Infrastructure.CloudStorage;

public interface ICloudStorageService
{
    public Task<string> UploadFileAsync(IFormFile file, Guid userId);
}
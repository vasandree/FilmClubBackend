using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using UserService.Infrastructure.CloudStorage;

namespace UserService.Infrastructure.Configurations;

public static class Configurations
{
    public static void ConfigureUserServiceInfrastructure(this WebApplicationBuilder builder)
    {
        CloudinaryConfiguration.Initialize();
        var cloudinary = CloudinaryConfiguration.GetCloudinaryInstance();
        builder.Services.AddSingleton(cloudinary);
        
        builder.Services.AddScoped<ICloudStorageService, CloudStorageService>();
        
    }
}
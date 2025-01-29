using CloudinaryDotNet;
using dotenv.net;
using System;

namespace UserService.Infrastructure.Configurations
{
    public static class CloudinaryConfiguration
    {
        private static Cloudinary _cloudinary;

        public static void Initialize()
        {
            try
            {
                DotEnv.Load(options: new DotEnvOptions(probeForEnv: true, ignoreExceptions: false));
                
                var apiKey = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY");
                var apiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET");
                var cloudName = Environment.GetEnvironmentVariable("CLOUDINARY_CLOUD_NAME");

                if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret) ||
                    string.IsNullOrEmpty(cloudName) || string.IsNullOrEmpty(cloudName))
                {
                    throw new Exception("API Key or API Secret or Cloud name is not set.");
                }

                Account account = new Account(
                    cloudName,
                    apiKey,
                    apiSecret
                );

                _cloudinary = new Cloudinary(account);
                _cloudinary.Api.Secure = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during initialization: {ex.Message}");
                throw;
            }
        }

        public static Cloudinary GetCloudinaryInstance()
        {
            if (_cloudinary == null)
            {
                throw new InvalidOperationException("Cloudinary has not been initialized. Call Initialize() first.");
            }

            return _cloudinary;
        }
    }
}
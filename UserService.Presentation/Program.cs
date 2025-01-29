using Common.Configurations.Configurations;
using UserService.Application.Configurators;
using UserService.Infrastructure.Configurations;
using UserService.Persistence.Configurations;

var builder = WebApplication.CreateBuilder(args);

try
{
    builder.ConfigureUserDb();
    builder.ConfigureRedisDb();
    builder.ConfigureRepositories();
    builder.ConfigureUserServiceApplication();
    builder.ConfigureAuth();
    builder.ConfigureIdentity();
    builder.ConfigureSwagger();
    builder.ConfigureUserServiceInfrastructure();
    
}
catch (Exception ex)
{
    Console.WriteLine($"Error during initialization: {ex.Message}");
    throw;
}


var app = builder.Build();

try
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    await app.ConfigureUserDb(); 
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.UseMiddleware();

    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"Error during application startup: {ex.Message}");
    throw; 
}
using Common.Configurations.Configurations;
using UserService.Application.Configurators;
using UserService.Persistence.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureUserDb();

builder.ConfigureRepositories();

builder.ConfigureUserServiceApplication();

builder.ConfigureAuth();

builder.ConfigureIdentity();

builder.ConfigureSwagger();

var app = builder.Build();

app.ConfigureUserDb();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware();

app.Run();
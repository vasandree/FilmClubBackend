using UserService.Persistence.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureUserDb();
builder.ConfigureRepositories();

var app = builder.Build();

app.ConfigureUserDb();

app.Run();

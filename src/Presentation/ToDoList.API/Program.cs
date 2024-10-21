using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.ApiKey;
using ToDoList.Infrastructure.Data;
using ToDoList.API.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
string connString = builder.Configuration.GetConnectionString("ToDoDB") ?? string.Empty;
builder.Services.AddDbContext<ToDoDbContext>(options =>
    options.UseMySql(connString,
        ServerVersion.AutoDetect(connString), x =>
        x.MigrationsAssembly("ToDoList.Infrastructure")
            .EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)));
builder.Services.AddSingleton<IApiKeyValidator, ApiKeyValidator>();
builder.Services.AddSingleton<IAuthorizationFilter, ApiKeyAuthFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthorization();
app.MapControllers();
app.MapTestEndpoints();
app.Run();
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.ApiKey;
using ToDoList.Infrastructure.Data;

namespace ToDoList.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<ToDoDbContext>(o =>
                o.UseSqlServer(builder.Configuration.GetConnectionString("ToDoDB"),
                               x => x.MigrationsAssembly("ToDoList.Infrastructure")));

            builder.Services.AddSingleton<IApiKeyValidator, ApiKeyValidator>();
            builder.Services.AddSingleton<IAuthorizationFilter, ApiKeyAuthFilter>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
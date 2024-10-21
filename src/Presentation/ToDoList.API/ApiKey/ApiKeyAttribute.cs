using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ToDoList.API.ApiKey
{
    // https://code-maze.com/aspnetcore-api-key-authentication/
    public class ApiKeyAttribute : ServiceFilterAttribute
    {
        public ApiKeyAttribute() : base(typeof(IAuthorizationFilter))
        {
        }
    }
}

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.API.ApiKey
{
    public class ApiKeyAuthFilter : IAuthorizationFilter
    {
        private readonly IApiKeyValidator _apiKeyValidator;

        public ApiKeyAuthFilter(IApiKeyValidator apiKeyValidator)
        {
            _apiKeyValidator = apiKeyValidator;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            const string apiKeyHeaderName = "X-API-Key";
            string? userApiKey = context.HttpContext.Request.Headers[apiKeyHeaderName];
            if (!_apiKeyValidator.IsValid(userApiKey))
            {
                context.Result = new UnauthorizedResult();
            }  
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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
            const string API_KEY_HEADERNAME = "X-API-Key";
            string? userApiKey = context.HttpContext.Request.Headers[API_KEY_HEADERNAME];
            if (!_apiKeyValidator.IsValid(userApiKey))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}

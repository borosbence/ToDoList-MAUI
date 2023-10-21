namespace ToDoList.API.ApiKey
{
    public interface IApiKeyValidator
    {
        bool IsValid(string? apiKey);
    }

    public class ApiKeyValidator : IApiKeyValidator
    {
        private readonly IConfiguration _configuration;
        public ApiKeyValidator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool IsValid(string? userApiKey)
        {
            const string apiKeyName = "ApiKey";
            if (string.IsNullOrWhiteSpace(userApiKey))
            {
                return false;
            }
            string? apiKey = _configuration.GetValue<string>(apiKeyName);
            if (apiKey == null || apiKey != userApiKey)
            {
                return false;
            }
            return true;
        }
    }
}

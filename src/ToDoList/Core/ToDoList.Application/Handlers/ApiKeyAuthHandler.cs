namespace ToDoList.Application.Handlers
{
    public class ApiKeyAuthHandler : DelegatingHandler
    {
        private const string API_KEY_HEADERNAME = "X-API-Key";
        private readonly string _apiKey;

        public ApiKeyAuthHandler(string apiKey)
        {
            _apiKey = apiKey;
            InnerHandler = new HttpClientHandler();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add(API_KEY_HEADERNAME, _apiKey);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}

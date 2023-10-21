using System.Net.Http.Headers;

namespace ToDoList.Application.Repositories
{
    public class ApiRepositoryBase
    {
        protected string _baseUrl;
        protected string _path;
        protected HttpClient client;

        public ApiRepositoryBase(string path, DelegatingHandler handler, string? baseUrl = null)
        {
            // TODO: read from appsettings
            _baseUrl = baseUrl ?? "http://localhost:5000/";
            _path = path;

            client = new HttpClient(handler)
            {
                BaseAddress = new Uri(_baseUrl)
            };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}

using System.Net.Http.Json;

namespace ToDoList.Application.Repositories
{
    public class GenericAPIRepository<T> : ApiRepositoryBase, IGenericRepository<T>
    {
        public GenericAPIRepository(string path, DelegatingHandler handler, string? baseUrl = null) : base(path, handler, baseUrl)
        {
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await client.GetFromJsonAsync<List<T>>(_path) ?? Enumerable.Empty<T>().ToList();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await client.GetFromJsonAsync<T>(_path + "/" + id);
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            var result = await client.GetAsync(_path + "/" + id);
            return result.IsSuccessStatusCode;
        }

        public async Task InsertAsync(T entity)
        {
            await client.PostAsJsonAsync(_path, entity);
        }

        public async Task UpdateAsync(int id, T entity)
        {
            await client.PutAsJsonAsync(_path + "/" + id, entity);
        }

        public async Task DeleteAsync(int id)
        {
            await client.DeleteAsync(_path + "/" + id);
        }
    }
}

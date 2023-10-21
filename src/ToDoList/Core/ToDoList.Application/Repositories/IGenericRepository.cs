using ToDoList.Domain.Entities;

namespace ToDoList.Application.Repositories
{
    public interface IGenericRepository<T> where T : IEntity
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<bool> ExistsByIdAsync(int id);
        Task<int> InsertAsync(T entity);
        Task UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);
    }
}
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Domain.Entities
{
    public interface IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}

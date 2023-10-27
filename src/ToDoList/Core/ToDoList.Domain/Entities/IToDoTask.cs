﻿namespace ToDoList.Domain.Entities
{
    public interface IToDoTask : IEntity
    {
        string Title { get; set; }
        string? Content { get; set; }
        DateTime? DeadLine { get; set; }
        bool IsCompleted { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime ModifiedDate { get; set; }
    }
}
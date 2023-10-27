using CommunityToolkit.Mvvm.ComponentModel;
using ToDoList.Domain.Entities;

namespace ToDoList.MAUI.Models
{
    public partial class ToDoTaskModel : ObservableObject, IToDoTask
    {
        [ObservableProperty]
        private int _id;
        [ObservableProperty]
        private string _title = null!;
        [ObservableProperty]
        private string? _content;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsCloseDL))]
        private DateTime? _deadLine;
        [ObservableProperty]
        private bool _isCompleted;
        [ObservableProperty]
        private DateTime _createdDate = DateTime.Now;
        [ObservableProperty]
        private DateTime _modifiedDate = DateTime.Now;

        public bool IsCloseDL => !IsCompleted && DeadLine.HasValue && DeadLine.Value.Date <= DateTime.Now.Date.AddDays(3);
    }
}
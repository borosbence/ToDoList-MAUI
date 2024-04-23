using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ToDoList.Application.Repositories;
using ToDoList.MAUI.Messages;
using ToDoList.MAUI.Models;

namespace ToDoList.MAUI.ViewModels
{
    [QueryProperty("Task", "Details")]
    public partial class TaskDetailViewModel : ObservableRecipient
    {
        private readonly IGenericRepository<TaskModel> _repository;

        public TaskDetailViewModel(IGenericRepository<TaskModel> repository)
        {
            _repository = repository;
        }

        [ObservableProperty]
        private bool _hasDeadLine;

        [ObservableProperty]
        private string? _pageTitle;

        private TaskModel _task = new();
        public required TaskModel Task
        {
            get { return _task; }
            set
            {
                value.Content = SetNewLines(value.Content);
                SetProperty(ref _task, value);
                HasDeadLine = Task.DeadLine != null;
                PageTitle = Task.Id == 0 ? "Jegyzet létrehozása" : "Jegyzet módosítása";
            }
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            bool exists = await _repository.ExistsByIdAsync(Task.Id);
            if (exists)
            {
                Task.ModifiedDate = DateTime.Now;
                await _repository.UpdateAsync(Task.Id, Task);
            }
            else
            {
                int newId = await _repository.InsertAsync(Task);
                Task.Id = newId;
                Messenger.Send(new MainPageMessage(new TaskMessage(Task)));
            }
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task DeleteAsync()
        {
            bool exists = await _repository.ExistsByIdAsync(Task.Id);
            if (exists)
            {
                await _repository.DeleteAsync(Task.Id);
            }
            Messenger.Send(new MainPageMessage(new TaskMessage(Task, ListAction.Delete)));
            await Shell.Current.GoToAsync("..");
        }

        private string? SetNewLines(string? text)
        {
#if __MOBILE__
            text?.Replace("\n", Environment.NewLine);
#endif
            return text;
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ToDoList.Application.Repositories;
using ToDoList.MAUI.Messages;
using ToDoList.MAUI.Models;
using sTask = System.Threading.Tasks;

namespace ToDoList.MAUI.ViewModels
{
    public partial class TaskDetailViewModel : ObservableValidator, IQueryAttributable
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

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotLoading))]
        private bool _isLoading;
        public bool IsNotLoading => !IsLoading;

        private TaskModel _task = new();
        public required TaskModel Task
        {
            get { return _task; }
            set
            {
                value.Content = value.Content?.ReplaceLineEndings();
                SetProperty(ref _task, value);
                OnPropertyChanged(nameof(Title));
                HasDeadLine = Task.DeadLine != null;
            }
        }

        public string? Title
        {
            get => _task?.Title;
            set 
            {
                SetProperty(_task.Title, value, _task, (o, n) => o.Title = n, true);
                SaveCommand.NotifyCanExecuteChanged();
            }
        }

        private bool CanSave() => !string.IsNullOrWhiteSpace(Title);

        [RelayCommand(CanExecute = nameof(CanSave))]
        private async Task SaveAsync()
        {
            IsLoading = true;
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
                WeakReferenceMessenger.Default.Send(new MainPageMessage(new TaskMessage(Task)));
            }
            IsLoading = false;
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
            WeakReferenceMessenger.Default.Send(new MainPageMessage(new TaskMessage(Task, ListAction.Delete)));
            await Shell.Current.GoToAsync("..");
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Task = query["Details"] as TaskModel ?? new TaskModel();
            PageTitle = Task.Id == 0 ? "Jegyzet létrehozása" : "Jegyzet módosítása";
        }
    }
}

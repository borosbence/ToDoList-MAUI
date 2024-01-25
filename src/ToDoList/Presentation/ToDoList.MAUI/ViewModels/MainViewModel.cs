using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using ToDoList.Application.Repositories;
using ToDoList.MAUI.Messages;
using ToDoList.MAUI.Models;
using ToDoList.MAUI.Views;

namespace ToDoList.MAUI.ViewModels
{
    public partial class MainViewModel : ObservableRecipient, IRecipient<MainPageMessage>
    {
        private readonly IGenericRepository<TaskModel> _repository;

        public MainViewModel(IGenericRepository<TaskModel> repository)
        {
            _repository = repository;
            Tasks = new ObservableCollection<TaskModel>();
        }

        public ObservableCollection<TaskModel> Tasks { get; set; }
        [ObservableProperty]
        public bool _isFirstLoad = true;
        [ObservableProperty]
        private bool _isRefreshing;

        [RelayCommand]
        private async Task LoadDataAsync()
        {
            if (!IsFirstLoad)
            {
                IsRefreshing = true;
            }
            var result = await _repository.GetAllAsync();
            Tasks.Clear();
            result.ForEach(Tasks.Add);
            IsFirstLoad = false;
            IsRefreshing = false;
        }

        [RelayCommand]
        private async Task ShowItemAsync(TaskModel Task)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Details", Task }
            };
            await Shell.Current.GoToAsync(nameof(TaskDetailPage), navigationParameter);
        }

        [RelayCommand]
        private async Task AddItemAsync()
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Details", new TaskModel() }
            };
            await Shell.Current.GoToAsync(nameof(TaskDetailPage), navigationParameter);
        }

        public void Receive(MainPageMessage message)
        {
            var mp_message = message.Value;
            if (mp_message.Action == ListAction.Add && mp_message.Data.Id > 0)
            {
                Tasks.Add(mp_message.Data);
            }
            else if (mp_message.Action == ListAction.Delete)
            {
                Tasks.Remove(mp_message.Data);
            }
        }
    }
}

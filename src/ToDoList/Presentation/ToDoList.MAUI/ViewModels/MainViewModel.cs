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
    public partial class MainViewModel : ObservableObject
    {
        private readonly IGenericRepository<ToDoTaskModel> _repository;

        public MainViewModel(IGenericRepository<ToDoTaskModel> repository)
        {
            _repository = repository;
            Tasks = new ObservableCollection<ToDoTaskModel>();
            RegisterUpdate();
        }

        public ObservableCollection<ToDoTaskModel> Tasks { get; set; }
        public bool IsFirstLoad { get; set; } = true;
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
        private async Task ShowItemAsync(ToDoTaskModel todoTask)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Details", todoTask }
            };
            await Shell.Current.GoToAsync(nameof(TaskDetailPage), navigationParameter);
        }

        [RelayCommand]
        private async Task AddItemAsync()
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Details", new ToDoTaskModel() }
            };
            await Shell.Current.GoToAsync(nameof(TaskDetailPage), navigationParameter);
        }

        private void RegisterUpdate()
        {
            WeakReferenceMessenger.Default.Register<MainPageMessage>(this, (r, m) =>
            {
                if (m.Value.Action == ListAction.Add && m.Value.Data.Id > 0)
                {
                    Tasks.Add(m.Value.Data);
                }
                else if (m.Value.Action == ListAction.Delete)
                {
                    Tasks.Remove(m.Value.Data);
                }
            });
        }
    }
}

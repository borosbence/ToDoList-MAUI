using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ToDoList.Application.Repositories;
using ToDoList.Domain.Entities;
using ToDoList.MAUI.Views;

namespace TeendokLista.MAUI.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IGenericRepository<ToDoTask> _repository;

        public MainViewModel(IGenericRepository<ToDoTask> repository)
        {
            _repository = repository;
            Tasks = new ObservableCollection<ToDoTask>();
            RegisterUpdate();
        }

        public ObservableCollection<ToDoTask> Tasks { get; set; }

        public async Task LoadData()
        {
            Tasks.Clear();
            var result = await _repository.GetAllAsync();
            result.ForEach(Tasks.Add);
        }

        private void RegisterUpdate()
        {
            // TODO: regisztrálás
            //MessagingCenter.Subscribe<TaskDetailViewModel, ToDoTask>(this, "UpdateView", async (sender, feladat) =>
            //{
            //    await LoadData();
            //});
        }

        [RelayCommand]
        private async Task ShowItemAsync(ToDoTask task)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Details", task }
            };
            await Shell.Current.GoToAsync(nameof(TaskDetailPage), navigationParameter);
        }

        [RelayCommand]
        private async Task AddItemAsync()
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Details", new ToDoTask() }
            };
            await Shell.Current.GoToAsync(nameof(TaskDetailPage), navigationParameter);
        }
    }
}

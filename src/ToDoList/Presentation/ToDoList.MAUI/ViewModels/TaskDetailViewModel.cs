using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ToDoList.Application.Repositories;
using ToDoList.Domain.Entities;
using ToDoList.MAUI.Views;

namespace TeendokLista.MAUI.ViewModels
{
    [QueryProperty(nameof(Task), "Details")]
    public partial class TaskDetailViewModel : ObservableObject
    {
        private readonly IGenericRepository<ToDoTask> _repository;

        public TaskDetailViewModel(IGenericRepository<ToDoTask> repository)
        {
            _repository = repository;
        }

        // TODO: model osztályon is notify
        [ObservableProperty]
        private ToDoTask _task;

        [RelayCommand]
        private async Task SaveAsync()
        {
            bool letezik = await _repository.ExistsByIdAsync(Task.Id);
            if (letezik)
            {
                await _repository.UpdateAsync(Task.Id, Task);
            }
            else
            {
                await _repository.InsertAsync(Task);
            }
            // TODO: csere
            MessagingCenter.Send(this, "UpdateView", Task);
            await Shell.Current.GoToAsync(nameof(MainPage));
        }

        [RelayCommand]
        private async Task DeleteAsync()
        {
            bool letezik = await _repository.ExistsByIdAsync(Task.Id);
            if (letezik)
            {
                await _repository.DeleteAsync(Task.Id);
            }
            MessagingCenter.Send(this, "UpdateView", Task);
            await Shell.Current.GoToAsync(nameof(MainPage));
        }
    }
}

﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ToDoList.Application.Repositories;
using ToDoList.MAUI.Messages;
using ToDoList.MAUI.Models;

namespace ToDoList.MAUI.ViewModels
{
    [QueryProperty("ToDoTask", "Details")]
    public partial class TaskDetailViewModel : ObservableObject
    {
        private readonly IGenericRepository<ToDoTaskModel> _repository;

        public TaskDetailViewModel(IGenericRepository<ToDoTaskModel> repository)
        {
            _repository = repository;
        }

        [ObservableProperty]
        private bool _hasDeadLine;

        [ObservableProperty]
        private string? _pageTitle;

        private ToDoTaskModel _toDoTask = new();
        public required ToDoTaskModel ToDoTask
        {
            get { return _toDoTask; }
            set
            {
                value.Content = SetNewLines(value.Content);
                SetProperty(ref _toDoTask, value);
                HasDeadLine = ToDoTask.DeadLine != null;
                PageTitle = ToDoTask.Id == 0 ? "Új jegyzet" : "Jegyzet módosítása";
            }
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            bool exists = await _repository.ExistsByIdAsync(ToDoTask.Id);
            if (exists)
            {
                ToDoTask.ModifiedDate = DateTime.Now;
                await _repository.UpdateAsync(ToDoTask.Id, ToDoTask);
            }
            else
            {
                int newId = await _repository.InsertAsync(ToDoTask);
                ToDoTask.Id = newId;
                WeakReferenceMessenger.Default.Send(new MainPageMessage(new ToDoTaskMessage(ToDoTask)));
            }
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task DeleteAsync()
        {
            bool exists = await _repository.ExistsByIdAsync(ToDoTask.Id);
            if (exists)
            {
                await _repository.DeleteAsync(ToDoTask.Id);
            }
            WeakReferenceMessenger.Default.Send(new MainPageMessage(new ToDoTaskMessage(ToDoTask, ListAction.Delete)));
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

using ToDoList.MAUI.ViewModels;

namespace ToDoList.MAUI.Views;

public partial class TaskDetailPage : ContentPage
{
    private readonly TaskDetailViewModel _vm;
    public TaskDetailPage(TaskDetailViewModel vm)
    {
        _vm = vm;
        BindingContext = _vm;
        InitializeComponent();
    }

    protected override async void OnDisappearing()
    {
        try
        {
            await _vm.SaveCommand.ExecuteAsync(null);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hiba", ex.Message, "OK");
        }
    }
}
using TeendokLista.MAUI.ViewModels;

namespace ToDoList.MAUI.Views;

public partial class TaskDetailPage : ContentPage
{
	public TaskDetailPage(TaskDetailViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}
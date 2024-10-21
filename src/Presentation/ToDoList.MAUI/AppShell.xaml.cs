using ToDoList.MAUI.Views;

namespace ToDoList.MAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(TaskDetailPage), typeof(TaskDetailPage));
        }
    }
}
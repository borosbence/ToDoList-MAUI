namespace ToDoList.MAUI
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            Current.UserAppTheme = AppTheme.Light;
        }
    }
}
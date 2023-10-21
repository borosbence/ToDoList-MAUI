using TeendokLista.MAUI.ViewModels;

namespace ToDoList.MAUI.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly MainViewModel _vm;
        public MainPage(MainViewModel vm)
        {
            _vm = vm;
            BindingContext = _vm;
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            try
            {
                await _vm.LoadData();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hiba", ex.Message, "OK");
            }
        }
    }
}
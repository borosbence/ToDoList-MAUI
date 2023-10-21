using Microsoft.Extensions.Logging;
using ToDoList.Application.Handlers;
using ToDoList.Application.Repositories;
using ToDoList.MAUI.Models;
using ToDoList.MAUI.ViewModels;
using ToDoList.MAUI.Views;

namespace ToDoList.MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif
            const string apiKey = "B3AAC4FA-2ACA-4040-AEC6-55FE96F4F31D";
            builder.Services.AddTransient<IGenericRepository<ToDoTaskModel>, GenericAPIRepository<ToDoTaskModel>>(x =>
            {
                return new("api/Tasks", new ApiKeyAuthHandler(apiKey));
            });
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<TaskDetailViewModel>();
            builder.Services.AddTransient<TaskDetailPage>();

            return builder.Build();
        }
    }
}
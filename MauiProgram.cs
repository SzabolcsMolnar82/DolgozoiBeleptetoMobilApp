using DolgozoiBeleptetoMobilApp.Views;
using DolgozoiBeleptetoMobilApp.ViewModels;
using Microsoft.Extensions.Logging;

namespace DolgozoiBeleptetoMobilApp
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

            // ViewModel-ek regisztrálása
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<CheckInViewModel>();
            builder.Services.AddSingleton<CheckOutViewModel>();
            builder.Services.AddSingleton<MonthlyHoursViewModel>();

            // Oldalak regisztrálása
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<CheckInPage>();
            builder.Services.AddSingleton<CheckOutPage>();
            builder.Services.AddSingleton<MonthlyHoursPage>();

            return builder.Build();
        }
    }

}

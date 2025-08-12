using Microsoft.Extensions.Logging;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Maps; // Add this using directive

using CourierSafetyApp.Services;
using CourierSafetyApp.ViewModels;

namespace CourierSafetyApp
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
              
            // Register Services
            builder.Services.AddSingleton<LocationService>();
            builder.Services.AddSingleton<EmergencyService>();

            // Register ViewModels
            builder.Services.AddTransient<MainViewModel>();

            // Register Pages
            builder.Services.AddTransient<MainPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
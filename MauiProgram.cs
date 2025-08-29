using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using zoft.MauiExtensions.Controls;
using CalorieCounter.Db;
using CalorieCounter.Pages;

namespace CalorieCounter
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseZoftAutoCompleteEntry()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<FoodInfoDbService>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<FoodInfo>();
#if DEBUG
            builder.Logging.AddDebug();
#endif


             return builder.Build();

         
        }
    }
}


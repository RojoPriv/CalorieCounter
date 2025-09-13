using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CalorieCounter.Db;
using CalorieCounter.Pages;
using Syncfusion.Maui.Core;
using Syncfusion.Maui.Core.Hosting;

namespace CalorieCounter
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1JFaF5cXGRCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWXZfc3VQR2BZU0Z1VkVWYEg=");
            var builder = MauiApp.CreateBuilder();
           
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<FoodInfoDbService>();
            builder.Services.AddSingleton<DailyTrackerDbService>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<FoodInfo>();
#if DEBUG
            builder.Logging.AddDebug();
#endif


             return builder.Build();

         
        }
    }
}


using CommunityToolkit.Maui;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using QuanLyDaiLy.DI;
using QuanLyDaiLy.Services;
using System.Reflection.PortableExecutable;

namespace QuanLyDaiLy
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.RegisterDependency();
            var appBuilder = builder.Build();
            appBuilder.Services.GetRequiredService<DatabaseService>().InitializeAsync();
            return appBuilder;
        }
    }
}

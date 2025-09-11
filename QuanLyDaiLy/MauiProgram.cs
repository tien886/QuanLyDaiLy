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
                    fonts.AddFont("MONTSERRAT-BOLD.TTF", "MontserratBold");
                    fonts.AddFont("MONTSERRAT-REGULAR.TTF", "MontserratRegular");
                    fonts.AddFont("MONTSERRAT-SEMIBOLD.TTF", "MontserratSemiBold");
                    fonts.AddFont("Font-Awesome-7-Free-Solid-900.otf", "FontAwesomeSolid");
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

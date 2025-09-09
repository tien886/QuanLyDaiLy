using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using QuanLyDaiLy.Configs;
using QuanLyDaiLy.Data;
using QuanLyDaiLy.Interfaces;
using QuanLyDaiLy.Repositories;
using QuanLyDaiLy.ServiceImpls;
using QuanLyDaiLy.Services;
using QuanLyDaiLy.ViewModels.DanhSachDaiLyViewModels;
using QuanLyDaiLy.Views;

namespace QuanLyDaiLy.DI;
public static class AppModule
{
    public static IServiceCollection RegisterDependency (this IServiceCollection services)
    {
        services.AddSingleton<DatabaseConfig>();
        services.AddDbContext<DataContext>((ServiceProvider, options) =>
        {
            var databasePath = DatabaseConfig.GetResourcePath();
            options.UseSqlite($"Data Source={databasePath}");
        });
        // Service Register
        services.AddScoped<DatabaseService, DatabaseServiceImpl>();
        services.AddScoped<IDaiLyService, DaiLyServiceImpl>();
        services.AddScoped<ILoaiDaiLyService, LoaiDaiLyServiceImpl>();
        services.AddScoped<IQuanService, QuanServiceImpl>();
        services.AddScoped<IThamSoService, ThamSoServiceImpl>();

        // Repository Register
        services.AddScoped<IDaiLyRepository, DaiLyRepository>();
        services.AddScoped<ILoaiDaiLyRepository, LoaiDaiLyRepository>();
        services.AddScoped<IQuanRepository, QuanRepository>();
        services.AddScoped<IThamSoRepository, ThamSoRepository>();
        // Views Register
        services.AddTransient<DanhSachDaiLyView>();
        services.AddTransient<ThemDaiLyView>();
        // ViewModels Register
        services.AddTransient<DanhSachDaiLyViewModel>();
        services.AddTransient<ThemDaiLyViewModel>();
        return services;
    }
}

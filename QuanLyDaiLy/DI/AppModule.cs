using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using QuanLyDaiLy.Configs;
using QuanLyDaiLy.Data;
using QuanLyDaiLy.Helper;
using QuanLyDaiLy.Interfaces;
using QuanLyDaiLy.Repositories;
using QuanLyDaiLy.ServiceImpls;
using QuanLyDaiLy.Services;
using QuanLyDaiLy.ViewModels.DanhSachDaiLyViewModels;
using QuanLyDaiLy.ViewModels.DanhSachPhieuXuatViewModels;
using QuanLyDaiLy.Views;
using QuanLyDaiLy.Views.DaiLyViews;
using QuanLyDaiLy.Views.DanhSachPhieuXuat;
using QuanLyDaiLy.Views.PhieuXuatViews;
using System.Diagnostics;

namespace QuanLyDaiLy.DI;
public static class AppModule
{
    public static IServiceCollection RegisterDependency (this IServiceCollection services)
    {
        Debug.WriteLine($"DONE register!!!!!!");
        services.AddSingleton<DatabaseConfig>();    
        services.AddDbContext<DataContext>((ServiceProvider, options) =>
        {
            var databasePath = DatabaseConfig.GetResourcePath();
            options.UseSqlite($"Data Source={databasePath}");
        });
        
        Debug.WriteLine($"Database Path: {DatabaseConfig.GetResourcePath()}");
        // Service Register
        services.AddScoped<DatabaseService, DatabaseServiceImpl>();
        services.AddScoped<IDaiLyService, DaiLyServiceImpl>();
        services.AddScoped<ILoaiDaiLyService, LoaiDaiLyServiceImpl>();
        services.AddScoped<IQuanService, QuanServiceImpl>();
        services.AddScoped<IThamSoService, ThamSoServiceImpl>();
        services.AddScoped<IMatHangService, MatHangServiceImplm>();
        services.AddScoped<IPhieuXuatService, PhieuXuatServiceImpl>();
        services.AddScoped<IDonViTinhService, DonViTinhServiceImpl>();
        services.AddScoped<ICTPhieuXuatService, CTPhieuXuatImpl>();
        // Repository Register
        services.AddScoped<IDaiLyRepository, DaiLyRepository>();
        services.AddScoped<ILoaiDaiLyRepository, LoaiDaiLyRepository>();
        services.AddScoped<IQuanRepository, QuanRepository>();
        services.AddScoped<IThamSoRepository, ThamSoRepository>();
        services.AddScoped<IMatHangRepository, MatHangRepository>();
        services.AddScoped<IPhieuXuatRepository, PhieuXuatRepository>();
        services.AddScoped<IDonViTinhRepository, DonViTinhRepository>();
        services.AddScoped<ICTPhieuXuatRepository, CTPhieuXuatRepository>();
        // Views Register
        services.AddTransient<DanhSachDaiLyView>();
        services.AddTransient<ThemDaiLyView>();
        services.AddTransient<DanhSachPhieuXuatView>();
        services.AddTransient<ThemPhieuXuatView>();
        services.AddTransient<TraCuuDaiLyView>();
        // ViewModels Register
        services.AddTransient<DanhSachDaiLyViewModel>();
        services.AddTransient<ThemDaiLyViewModel>();
        services.AddTransient<DanhSachPhieuXuatViewModel>();
        services.AddTransient<ThemPhieuXuatViewModel>();
        services.AddTransient<TraCuuDaiLyViewModel>();
        // Partitioner Register
        services.AddScoped<DanhSachDaiLyPartitioner>();
        return services;
    }
}

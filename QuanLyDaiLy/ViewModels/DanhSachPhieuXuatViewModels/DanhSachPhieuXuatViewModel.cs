
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuanLyDaiLy.Models;
using QuanLyDaiLy.Services;
using QuanLyDaiLy.Utils;
using QuanLyDaiLy.Views;
using QuanLyDaiLy.Views.PhieuXuatViews;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace QuanLyDaiLy.ViewModels.DanhSachPhieuXuatViewModels;

public partial class DanhSachPhieuXuatViewModel : BaseViewModel
{
    private readonly IPhieuXuatService phieuXuatService;
    private readonly IDaiLyService daiLyService;
    private readonly IServiceProvider serviceProvider;
    private readonly int pageSize = 30;
    [ObservableProperty]
    private ObservableCollection<PhieuXuat> dsPhieuXuat = [];
    [ObservableProperty]
    private int currentPage = 1;
    [ObservableProperty]
    private int totalPages;
    public DanhSachPhieuXuatViewModel(
            IPhieuXuatService phieuXuatService,
            IDaiLyService daiLyService,
            IServiceProvider serviceProvider
        )
    {
        this.phieuXuatService = phieuXuatService;
        this.daiLyService = daiLyService;
        this.serviceProvider = serviceProvider;
        _ = LoadDataAsync();
        _ = GetTotalPages();
        Debug.WriteLine($"Export Details: {TotalPages}");
    }
    [RelayCommand]
    public async Task AddButton()
    {
        try
        {
            var themPhieuXuatViewModel = serviceProvider.GetRequiredService<ThemPhieuXuatViewModel>();
            var newpopup = new ThemPhieuXuatView(themPhieuXuatViewModel);
            var mainPage = Application.Current?.MainPage;
            if (mainPage != null)
            {
                await mainPage.ShowPopupAsync(newpopup);
            }

        }
        catch (Exception ex)
        {
            await AlertUtil.ShowAlert($"Error: {ex.Message}");
        }
    }
    [RelayCommand]
    public async Task ExitButton()
    {
        await AppShell.Current.GoToAsync(nameof(DanhSachDaiLyView));
    }
    [RelayCommand]
    public async Task NextPageButton()
    {
        if (CurrentPage < TotalPages)
        {
            CurrentPage++;
            _ = LoadDataAsync();
            Debug.WriteLine(CurrentPage);

        }
    }
    [RelayCommand]
    public async Task PreviousPageButton()
    {
        if (CurrentPage > 1)
        {
            CurrentPage--;
            _ = LoadDataAsync();
            Debug.WriteLine(CurrentPage);
        }
    }
    private async Task GetTotalPages()
    {
        var phieuXuats = await phieuXuatService.GetAllPhieuXuatAsync();
        var dsPhieuXuat = new ObservableCollection<PhieuXuat>(phieuXuats);
        TotalPages = (int)Math.Ceiling((double)dsPhieuXuat.Count / pageSize);
    }
    public async Task LoadDataAsync()
    {
        try
        {   
            IsLoading = true;
            var loadList = await phieuXuatService.GetPhieuXuatsPaging(currentPage, pageSize);
            DsPhieuXuat = new ObservableCollection<PhieuXuat>(loadList);
        }
        catch (Exception ex)
        {
            await AlertUtil.ShowAlert($"Error loading data: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }
}

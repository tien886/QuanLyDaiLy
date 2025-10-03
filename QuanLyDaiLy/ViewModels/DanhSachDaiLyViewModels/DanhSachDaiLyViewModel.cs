
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuanLyDaiLy.Helper;
using QuanLyDaiLy.Models;
using QuanLyDaiLy.Services;
using QuanLyDaiLy.Utils;
using QuanLyDaiLy.ViewModels.DanhSachPhieuXuatViewModels;
using QuanLyDaiLy.Views;
using QuanLyDaiLy.Views.DaiLyViews;
using QuanLyDaiLy.Views.DanhSachPhieuXuat;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;

namespace QuanLyDaiLy.ViewModels.DanhSachDaiLyViewModels;

public partial class DanhSachDaiLyViewModel : BaseViewModel
{
    // Basic Initalization
    private readonly IDaiLyService _daiLyService;
    private readonly IServiceProvider _serviceProvider;
    [ObservableProperty]
    private ObservableCollection<DaiLy> dsDaiLy = [];
    private static bool InitLoad = false;
    private readonly int pageSize = 20;
    [ObservableProperty]
    private int currentPage = 1;
    [ObservableProperty]
    private int totalPages;
    public DanhSachDaiLyViewModel(IDaiLyService daiLyService, IServiceProvider serviceProvider)
    {
        this._daiLyService = daiLyService;
        this._serviceProvider = serviceProvider;   
        title = "DANH SÁCH ĐẠI LÝ";
        _ = LoadDataAsync();
        _ = GetTotalPage();
        Debug.WriteLine(totalPages);
    }

    // ButtonCommand Implement

    [RelayCommand]
    public async Task AbandondedButton()
    {
        string message = "THIS FUNCTION DOES NOT WORK";
        await AlertUtil.ShowAlert(message);
    }
    [RelayCommand]
    public async Task AddButton()
    {
        try
            {
            // Resolve viewmodel or link the two pages
            var themDaiLyViewModel = _serviceProvider.GetRequiredService<ThemDaiLyViewModel>();
            // Create a popup to add dealer 
            var newpopup = new ThemDaiLyView(themDaiLyViewModel);

            var mainpage = Application.Current?.MainPage;
            if(mainpage != null)
            {
                IsLoading = true;
                await mainpage.ShowPopupAsync(newpopup);
                _ = LoadDataAsync();
                IsLoading = false;
            }
        }
        catch (Exception ex)
        {
            await AlertUtil.ShowAlert($"Error opening add dealer popup: {ex.Message}");
        }

    }
    [RelayCommand]
    public async Task ExportButton()
    {
        try
        {
            await AppShell.Current.GoToAsync(nameof(DanhSachPhieuXuatView));
        }
        catch
        {
            AlertUtil.ShowAlert("Cannot navigate to Export Page" );
        }
    }

    [RelayCommand]
    public async Task LoadButton()
    {
        await Task.Yield();
        try
        {
            
            await Task.Run(async () =>
            {
                await LoadDataAsync();
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading data: {ex.Message}");
        }   
        finally
        {
            Debug.WriteLine("Loading stop");

            await Task.Yield();
        }
        Debug.WriteLine("LoadDataButton on");
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

    private async Task LoadDataAsync()
    {
        //await Task.Delay(3000);
        IsLoading = true;
        var loadList = await _daiLyService.GetDailiesPaging(CurrentPage, pageSize);
        DsDaiLy = new ObservableCollection<DaiLy>(loadList);    
        Debug.WriteLine("Load data Succesfully");
        IsLoading = false;
    }
    // Paging

    private async Task GetTotalPage()
    {
        var dailies = await _daiLyService.GetAllDaiLyAsync();
        var dsDaiLy = new ObservableCollection<DaiLy>(dailies);
        TotalPages = (int)Math.Ceiling((double)dsDaiLy.Count/pageSize);
        Debug.WriteLine("TOTAL PAGES: ", TotalPages);
    }

    [RelayCommand]
    private async Task LookUpButton()
    {
        try
        {
            var traCuuDaiLyViewModel = _serviceProvider.GetRequiredService<TraCuuDaiLyViewModel>();
            var newpopup = new TraCuuDaiLyView(traCuuDaiLyViewModel);
            var mainpage = Application.Current?.MainPage;
            if (mainpage != null)
            {
                IsLoading = true;
                await mainpage.ShowPopupAsync(newpopup);
                IsLoading = false;
            }
        }
        catch (Exception ex)
        {
            await AlertUtil.ShowAlert($"Error opening lookup dealer popup: {ex.Message}");
        }
    }


}

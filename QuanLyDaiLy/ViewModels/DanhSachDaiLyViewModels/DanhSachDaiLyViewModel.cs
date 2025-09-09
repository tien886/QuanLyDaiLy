
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuanLyDaiLy.Models;
using QuanLyDaiLy.Services;
using QuanLyDaiLy.Utils;
using QuanLyDaiLy.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace QuanLyDaiLy.ViewModels.DanhSachDaiLyViewModels;

public partial class DanhSachDaiLyViewModel : BaseViewModel
{
    // Basic Initalization
    private readonly IDaiLyService _daiLyService;
    private readonly IServiceProvider _serviceProvider;
    [ObservableProperty]
    private ObservableCollection<DaiLy> dsDaiLy = []; 
    public DanhSachDaiLyViewModel(IDaiLyService daiLyService, IServiceProvider serviceProvider)
    {
        this._daiLyService = daiLyService;
        this._serviceProvider = serviceProvider;
        title = "DANH SÁCH ĐẠI LÝ";
        _ = LoadDataAsync();
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
                await mainpage.ShowPopupAsync(newpopup);
                await LoadDataAsync();
            }
        }
        catch (Exception ex)
        {
            await AlertUtil.ShowAlert($"Error opening add dealer popup: {ex.Message}");
        }

    }
    [RelayCommand]
    public async Task LoadButton()
    {
        IsLoading = true;
        try
        {
            _ = LoadDataAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading data: {ex.Message}");
        }   
        finally
        {
            if (IsLoading)
                Debug.WriteLine("Loading Data");
            else
                Debug.WriteLine("Loading Data Failed");
            IsLoading = false;
        }
        Debug.WriteLine("LoadDataButton on");
    }

    private async Task LoadDataAsync()
    {
        var loadList = await _daiLyService.GetAllDaiLyAsync();
        DsDaiLy = new ObservableCollection<DaiLy>(loadList);
    }

}

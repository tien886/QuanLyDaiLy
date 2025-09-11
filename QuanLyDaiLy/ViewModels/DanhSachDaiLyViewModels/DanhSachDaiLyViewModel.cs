
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuanLyDaiLy.Models;
using QuanLyDaiLy.Services;
using QuanLyDaiLy.Utils;
using QuanLyDaiLy.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

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
                IsLoading = true;
                await LoadDataAsync();
                IsLoading = false;
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
        await Task.Yield();
        try
        {
            
            await Task.Run(async () =>
            {
                var loadList = await _daiLyService.GetAllDaiLyAsync();
                DsDaiLy = new ObservableCollection<DaiLy>(loadList);
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading data: {ex.Message}");
        }   
        finally
        {
            Debug.WriteLine("Loading stop");

            IsLoading = false;
            await Task.Yield();
        }
        Debug.WriteLine("LoadDataButton on");
    }

    private async Task LoadDataAsync()
    {
        //await Task.Delay(3000);
        var loadList = await _daiLyService.GetAllDaiLyAsync();
        DsDaiLy = new ObservableCollection<DaiLy>(loadList);
        Debug.WriteLine("Load data Succesfully");
    }

}

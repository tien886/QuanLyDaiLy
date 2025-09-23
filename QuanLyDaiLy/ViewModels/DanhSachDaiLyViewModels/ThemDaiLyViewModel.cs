
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Query.Internal;
using QuanLyDaiLy.Models;
using QuanLyDaiLy.Services;
using QuanLyDaiLy.Utils;
using QuanLyDaiLy.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace QuanLyDaiLy.ViewModels.DanhSachDaiLyViewModels;

public partial class ThemDaiLyViewModel : BaseViewModel
{
    private readonly IDaiLyService daiLyService;
    private readonly ILoaiDaiLyService loaiDaiLyService;
    private readonly IQuanService quanService;
    private readonly IThamSoService thamSoService;
    private Popup? currentPopup;

    public ThemDaiLyViewModel(
        IDaiLyService daiLyService,
        ILoaiDaiLyService loaiDaiLyService,
        IQuanService quanService,
        IThamSoService thamSoService
        )
    {
        this.daiLyService = daiLyService;
        this.loaiDaiLyService = loaiDaiLyService;
        this.quanService = quanService;
        this.thamSoService = thamSoService;
        title = "TIẾP NHẬN ĐẠI LÝ";   
        
        _ = LoadScreen();
    }
    public void SetPopupReference(Popup popup)
    {
        currentPopup = popup;
    }
    //  Create properties here
    [ObservableProperty]
    private int maDaiLy;
    [ObservableProperty]
    private string ten = string.Empty;
    [ObservableProperty]
    private LoaiDaiLy? selectedLoaiDaiLy;
    [ObservableProperty]
    private ObservableCollection<LoaiDaiLy> loaiDailies = [];
    [ObservableProperty]
    private Quan? selectedQuan ;
    [ObservableProperty]
    private ObservableCollection<Quan> quans = [];
    [ObservableProperty]
    private string soDienThoai = string.Empty;
    [ObservableProperty]
    private string email = string.Empty;
    [ObservableProperty]
    private DateTime ngayTiepNhan = DateTime.Now;
    [ObservableProperty]
    private string diaChi = string.Empty;
    [ObservableProperty]
    private int soLuongDaiLyHienCo;



    // Command Implements
    [RelayCommand]
    public async Task CloseButton()
    {
        await Close();
    }

    [RelayCommand]
    public async Task XButton()
    {
        await Close();
    }

    [RelayCommand]

    public async Task RefreshButton()
    {
        await Refresh();
    }

    [RelayCommand]
    public async Task AddButton()
    {
        if (await CheckValidDealer())
        {
            IsLoading = true;
            try
            {
                var newDaiLy = new DaiLy
                {
                    MaDaiLy = MaDaiLy,
                    Ten = ten,
                    Email = Email,
                    SoDienThoai = SoDienThoai,
                    DiaChi = DiaChi,
                    NgayTiepNhan = NgayTiepNhan,
                    Quan = SelectedQuan,
                    LoaiDaiLy = SelectedLoaiDaiLy,
                };
                try
                {
                    await daiLyService.AddDaiLyAsync(newDaiLy);
                    AlertUtil.ShowSuccess("INSERTING DEALER SUCCESSFUL");
                }
                catch(Exception ex)
                {
                    AlertUtil.ShowAlert("INSERTING DEALER FAILED"); 
                }
                IsLoading = false;
            }
            catch (Exception ex)
            {
                AlertUtil.ShowAlert($"SOME THING WENT WRONG WITH DATA ENTRY: {ex}");
            }
            if (currentPopup != null)
                _ = currentPopup.CloseAsync();
        }
        
    }

    // Additional Fucntion
    public async Task LoadScreen()
    {
        MaDaiLy = await daiLyService.GetNextAvailableIdAsync();
        _ = LoadDataToScreen();
    }

    public async Task LoadDataToScreen()
    {
        try
        {
            IsLoading = true;
            var loaiDailies = await loaiDaiLyService.GetAllLoaiDaiLyAsync();
            var quans = await quanService.GetAllQuanAsync();

            LoaiDailies = new ObservableCollection<LoaiDaiLy>(loaiDailies);
            Quans = new ObservableCollection<Quan>(quans);

            if (loaiDailies.Any())
            {
                SelectedLoaiDaiLy = LoaiDailies[0];
            }
            if(quans.Any())
            {
                SelectedQuan = null;
                await LoadSoLuongDaiLyHienCoAsync();
            }
        }
        catch(Exception ex)
        {
            await AlertUtil.ShowAlert($"Lỗi khi tải dữ liệu: {ex.Message}");
        }
    }
     
    public async Task LoadSoLuongDaiLyHienCoAsync()
    {
        if (SelectedQuan != null)
        {
            try
            {
                var allDailies = await daiLyService.GetAllDaiLyAsync();
                SoLuongDaiLyHienCo = allDailies.Count(dl => dl.MaQuan == SelectedQuan.MaQuan);
                int SoLuongDaiLyToiDa = await GetMaxNumberOfDealerWarning();
                Debug.WriteLine($"Max Dealer = {SoLuongDaiLyToiDa}");
                Debug.WriteLine($"Current Dealer = {SoLuongDaiLyHienCo}");
                if (SoLuongDaiLyHienCo >= SoLuongDaiLyToiDa)
                {
                    string QuanMaxDealers = selectedQuan.TenQuan;
                    Debug.WriteLine("Over the bound");
                    await SetQuanDefault();
                    await AlertUtil.ShowAlert($"REACHED THE MAXIMUM OF DEALERS {QuanMaxDealers}");
                    await SetNumberOfExistDealersDefault();
                }
            }

            catch (Exception ex)
            {
                var message = $"Error in load number of dealers in Quans: {ex}";
                AlertUtil.ShowAlert(message);
            }
        }
        else
            SoLuongDaiLyHienCo = 0;

    }
    partial void OnSelectedQuanChanged(Quan? value)
    {
        Debug.WriteLine($"Changed Quan");
        _ = LoadSoLuongDaiLyHienCoAsync();
    }
    private async Task<int> GetMaxNumberOfDealerWarning()
    {
        try
        {
            var soLuongToiDaStr = await thamSoService.GetThamSo("SoLuongDaiLyToiDa");
            if (!string.IsNullOrEmpty(soLuongToiDaStr) && int.TryParse(soLuongToiDaStr, out int soLuongToiDa))
            {
                return soLuongToiDa;
            }
        }
        catch (Exception ex)
        {
            await AlertUtil.ShowAlert($"Lỗi khi tải số lượng đại lý tối đa: {ex.Message}");
        }
        return 3;
    }
    private async Task SetQuanDefault()
    {
        selectedQuan = null;
    }
    private async Task SetNumberOfExistDealersDefault()
    {
        SoLuongDaiLyHienCo = 0;
    }
    public async Task Close()
    {
        if (currentPopup != null)
        {
            await currentPopup.CloseAsync();
        }
    }

    public async Task Refresh()
    {
        Ten = string.Empty;
        SoDienThoai = string.Empty;
        Email = string.Empty;
        DiaChi = string.Empty;
        await SetNumberOfExistDealersDefault();
        SelectedQuan = null;
        SelectedLoaiDaiLy = LoaiDailies[0];
        NgayTiepNhan = DateTime.Now;
    }

    public async Task<bool> CheckValidDealer()
    {
        if (string.IsNullOrWhiteSpace(Ten))
        {
            await AlertUtil.ShowAlert("Enter Dealer Name");
            return false;
        }
        if(string.IsNullOrEmpty(DiaChi))
        {
            await AlertUtil.ShowAlert("Enter Address");
            return false;
        }
        if (string.IsNullOrWhiteSpace(Email))
        {
            await AlertUtil.ShowAlert("Enter Email");
            return false;
        }
        if (string.IsNullOrWhiteSpace(SoDienThoai))
        {
            await AlertUtil.ShowAlert("Enter Phone Number");
            return false;
        }
        if (SelectedLoaiDaiLy == null)
        {
            await AlertUtil.ShowAlert("Choose Dealer Type");
            return false;
        }
        if (SelectedQuan == null)
        {
            await AlertUtil.ShowAlert("Choose District");
            return false;
        }
        return true;
    }
}

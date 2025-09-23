
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuanLyDaiLy.Models;
using QuanLyDaiLy.Models.DTO;
using QuanLyDaiLy.Services;
using QuanLyDaiLy.Utils;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace QuanLyDaiLy.ViewModels.DanhSachPhieuXuatViewModels;

public partial class ThemPhieuXuatViewModel : BaseViewModel
{
    private readonly IDaiLyService _daiLyService;
    private readonly IPhieuXuatService _phieuXuatService;
    private readonly IMatHangService _matHangService;
    private readonly ILoaiDaiLyService _loaiDaiLyService;
    private readonly ICTPhieuXuatService _ctPhieuXuatService;
    private Popup? currentPopup;
    public ThemPhieuXuatViewModel(
        IDaiLyService daiLyService,
        IPhieuXuatService phieuXuatService,
        IMatHangService matHangService,
        ILoaiDaiLyService loaiDaiLyService,
        ICTPhieuXuatService ctPhieuXuatService
        )
    {
        _daiLyService = daiLyService;
        _phieuXuatService = phieuXuatService;
        _matHangService = matHangService;
        _loaiDaiLyService = loaiDaiLyService;
        _ctPhieuXuatService = ctPhieuXuatService;
        _ = LoadDataAsync();
        selectedCTPhieuXuats.Add(new CTPhieuXuatDTO { STT = nextSTT });
    }
    [ObservableProperty]
    private ObservableCollection<CTPhieuXuatDTO> selectedCTPhieuXuats = new ();    //Store the CTPhieuXuat to be added to PhieuXuats
    private int nextSTT = 1; // counter for STT
    [ObservableProperty]
    private int maPhieuXuat;
    [ObservableProperty]
    private DaiLy selectedDaiLy;
    [ObservableProperty]
    private ObservableCollection<DaiLy> daiLies = [];
    [ObservableProperty]
    private ObservableCollection<MatHang> matHangs = [];
    [ObservableProperty]
    private DateTime ngayLap = DateTime.Now;
    [ObservableProperty]
    private long tongTien ;

    public void SetPopupReference(Popup popup)
    {
        currentPopup = popup;
    }
    public async Task LoadDataAsync()
    {

        maPhieuXuat = await _phieuXuatService.GetNextAvailableIdAsync();
        var Dailies = await _daiLyService.GetAllDaiLyAsync();
        DaiLies = new ObservableCollection<DaiLy>(Dailies);
        var Mathangs = await _matHangService.GetAllMatHangsAsync();
        MatHangs = new ObservableCollection<MatHang>(Mathangs);
    }
    [RelayCommand]
    public async Task ExitButton()
    {
        if(currentPopup != null)
            await currentPopup.CloseAsync();    
    }
    [RelayCommand]
    public async Task AddMatHangButton()
    {
        if (selectedCTPhieuXuats[nextSTT - 1].ThanhTien == 0 || selectedCTPhieuXuats[nextSTT - 1].SelectedMatHang == null)
        {
            AlertUtil.ShowFailed("Can not add a new CTPhieuXuat, check the entry");
            return;
        }
        long CurrentTongTien = 0;
        nextSTT++;
        selectedCTPhieuXuats.Add(new CTPhieuXuatDTO { STT = nextSTT });
        await TongTienCalc();
    }
    [ObservableProperty]
    private string editSelectedMatHang = "Xóa mặt hàng";
    [RelayCommand]
    public async Task EditMatHang()
    {
        if(EditSelectedMatHang == "Xóa mặt hàng")
        {
            EditSelectedMatHang = "Hủy";
            IsDeleting = true;
        }
        else
        {
            EditSelectedMatHang = "Xóa mặt hàng";
            IsDeleting = false;
        }
    }
    [RelayCommand]
    public async Task AddPhieuXuat()
    {
        if(selectedDaiLy == null)
        {
            AlertUtil.ShowAlert("CHOOSE A DAILY");
            return;
        }
        if(selectedCTPhieuXuats.Count == 0 || selectedCTPhieuXuats[0].SelectedMatHang == null )
        {
            AlertUtil.ShowFailed("FAILED TO EXPORT");
            return;
        }
        foreach(var item in selectedCTPhieuXuats)
        {
            if(item.SelectedMatHang == null || item.SoLuongXuat == "0")
            {
                AlertUtil.ShowFailed("FAILED TO EXPORT");
                return;
            }
        }
        // If the entry is in correct format 
        var phieuXuat = new PhieuXuat
        {
            MaPhieuXuat = MaPhieuXuat,
            MaDaiLy = selectedDaiLy.MaDaiLy,
            NgayLap = NgayLap,
            TongGiaTri = TongTien
        };
        await _phieuXuatService.AddPhieuXuatAsync(phieuXuat);
        foreach(var item in selectedCTPhieuXuats)
        {
            var newCTPhieuXuat = new CTPhieuXuat
            {
                MaCTPhieuXuat = await _ctPhieuXuatService.GetNextCTPhieuXuatAvailableIdAsync(),
                MaMatHang = item.SelectedMatHang.MaMatHang,
                MaPhieuXuat = MaPhieuXuat,
                SoLuongXuat = int.Parse(item.SoLuongXuat),
                DonGiaXuat = int.Parse(item.DonGia),
                ThanhTien = item.ThanhTien
            };
            // Update the MatHang stock
            var matHangToUpdate = await _matHangService.GetMatHangByID(item.SelectedMatHang.MaMatHang);
            matHangToUpdate.SoLuongTon -= newCTPhieuXuat.SoLuongXuat;
            await _matHangService.UpdateMatHang(matHangToUpdate);
            await _ctPhieuXuatService.AddCTPhieuXuatAsync(newCTPhieuXuat);

        }
        if (currentPopup != null)
            await currentPopup.CloseAsync();
    }
    [RelayCommand]
    public async Task DeleteMatHang()
    {
        var toRemove = selectedCTPhieuXuats.Where(x => x.IsSelected).ToList();
        foreach (var item in toRemove)
        {
            selectedCTPhieuXuats.Remove(item);
            
            nextSTT--;
            if(nextSTT == 0) // Init a primary object
            {
                nextSTT = 1;
                selectedCTPhieuXuats.Add(new CTPhieuXuatDTO { STT = nextSTT });
            }
        }
        await EditMatHang();
        await ResetSTT();
        await TongTienCalc();
    }
    [RelayCommand]
    public async Task SaveMatHangButton()
    {
        await TongTienCalc();
    }
    public async Task TongTienCalc()
    {
        long CurrentTongTien = 0;
        foreach (var items in selectedCTPhieuXuats)
        {
            CurrentTongTien += items.ThanhTien;

        }
        TongTien = CurrentTongTien;
    }

    public async Task ResetSTT()
    {
        int STTtmp = 1;
        foreach(var item in selectedCTPhieuXuats)
        {
            item.STT = STTtmp;
            STTtmp++;
        }
    }
    
}

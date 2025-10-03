
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuanLyDaiLy.Models;
using QuanLyDaiLy.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace QuanLyDaiLy.ViewModels.DanhSachDaiLyViewModels;

public partial class TraCuuDaiLyViewModel : BaseViewModel
{
    private readonly ILoaiDaiLyService loaiDaiLyService;
    private readonly IQuanService quanService;
    private readonly IMatHangService matHangService;
    private readonly IDonViTinhService donViTinhService;    
    private readonly IDaiLyService daiLyService;
    private Popup? currentPopup;
    public TraCuuDaiLyViewModel(
        ILoaiDaiLyService loaiDaiLyService,
        IQuanService quanService,
        IMatHangService matHangService,
        IDonViTinhService donViTinhService,
        IDaiLyService daiLyService
        )
    {
        this.loaiDaiLyService = loaiDaiLyService;
        this.quanService = quanService;
        this.matHangService = matHangService;
        this.donViTinhService = donViTinhService;
        this.daiLyService = daiLyService;
        _ = LoadScreen();
    }
    // Attributes
    // row 0
    [ObservableProperty]
    int maDaiLy = 0;
    [ObservableProperty]
    string tenDaiLy = string.Empty;
    [ObservableProperty]
    string soDienThoai = string.Empty;
    [ObservableProperty]
    string diaChi = string.Empty;
    [ObservableProperty]
    string email = string.Empty;
    [ObservableProperty]
    ObservableCollection<LoaiDaiLy> loaiDailies;
    [ObservableProperty]
    LoaiDaiLy selectedLoaiDaiLy;
    [ObservableProperty]
    ObservableCollection<Quan> quans;
    [ObservableProperty]
    Quan selectedQuans;
    // row 1
    [ObservableProperty]
    DateTime ngayTiepNhanBegin;
    [ObservableProperty]
    DateTime ngayTiepNhanEnd;
    [ObservableProperty]
    int noDaiLyBegin = 0;
    [ObservableProperty]
    int noDaiLyEnd = 0;
    // row 2
    [ObservableProperty]
    int noToiDaBegin = 0;
    [ObservableProperty]
    int noToiDaEnd = 0;
    [ObservableProperty]
    int maPhieuXuatBegin = 0;
    [ObservableProperty]
    int maPhieuXuatEnd = 0;
    // row 3
    [ObservableProperty]
    DateTime ngayLapPhieuXuatBegin;
    [ObservableProperty]
    DateTime ngayLapPhieuXuatEnd;
    [ObservableProperty]
    long tongGiaTriPXBegin = 0;
    [ObservableProperty]
    long tongGiaTriPXEnd = 0;
    // row 4
    [ObservableProperty]
    ObservableCollection<MatHang> matHangs;
    [ObservableProperty]
    MatHang selectedMatHang;
    [ObservableProperty]
    string tenDonViTinh;
    // row 5
    [ObservableProperty]
    int soLuongXuatBegin = 0;
    [ObservableProperty]
    int soLuongXuatEnd = 0;
    [ObservableProperty]
    int donGiaXuatBegin = 0;
    [ObservableProperty]
    int donGiaXuatEnd = 0;
    // row 6
    [ObservableProperty]
    int thanhTienBegin = 0;
    [ObservableProperty]
    int thanhTienEnd = 0;
    [ObservableProperty]
    int soLuongTonBegin = 0;
    [ObservableProperty]
    int soLuongTonEnd = 0;
    // Finding Result 
    [ObservableProperty]
    ObservableCollection<DaiLy> foundDailies;
    [ObservableProperty]
    int sTT = 0;    
    public void SetPopupReference(Popup popup)
    {
        currentPopup = popup;
    }
    [RelayCommand]
    public async Task CloseButton()
    {
        if (currentPopup != null)
        {
            await currentPopup.CloseAsync();
        }
    }
    public async Task LoadScreen()
    {
        var mathang = await matHangService.GetAllMatHangsAsync();
        var loaidaily = await loaiDaiLyService.GetAllLoaiDaiLyAsync();
        var quan = await quanService.GetAllQuanAsync();
        MatHangs = new ObservableCollection<MatHang>(mathang);
        LoaiDailies = new ObservableCollection<LoaiDaiLy>(loaidaily);
        Quans = new ObservableCollection<Quan>(quan);
    }
    partial void OnSelectedMatHangChanged(MatHang? newSelectedMatHang)
    {
        _ = LoadDVT();
    }
    public async Task LoadDVT()
    {
        TenDonViTinh = selectedMatHang.DonViTinh.TenDonViTinh;
    }
    [RelayCommand]
    public async Task LookUpDaiLy()
    {

        var allDaiLy = await daiLyService.GetAllDaiLyAsync();
        ObservableCollection<DaiLy> filteredResults = new ObservableCollection<DaiLy>(allDaiLy);

        int checkSize = filteredResults.Count;
        Debug.WriteLine(checkSize);
        // Tìm theo Mã đại lý
        if (!string.IsNullOrEmpty(MaDaiLy.ToString()))
        {
            filteredResults = [.. filteredResults.Where(d => d.MaDaiLy.ToString().Contains(MaDaiLy.ToString()))];
        }
        Debug.WriteLine(filteredResults.Count);

        // Tìm theo Tên đại lý
        if (!string.IsNullOrEmpty(TenDaiLy))
        {
            filteredResults = [.. filteredResults.Where(d => d.Ten.Contains(TenDaiLy))];
        }

        //// Tìm theo Số điện thoại
        if (!string.IsNullOrEmpty(SoDienThoai))
        {
            filteredResults = [.. filteredResults.Where(d => d.SoDienThoai.Contains(SoDienThoai))];
        }
        // Tìm theo địa chỉ
        if(!string.IsNullOrEmpty(DiaChi))
        {
            filteredResults = [.. filteredResults.Where(d => d.DiaChi.Contains(DiaChi))];
        }
        // Tìm theo Email
        if (!string.IsNullOrEmpty(Email))
        {
            filteredResults = [.. filteredResults.Where(d => d.Email.Contains(Email))];
        }
        Debug.WriteLine(filteredResults.Count);

        // Tìm theo Ngày tiếp nhận
        if (NgayTiepNhanBegin != DateTime.MinValue && NgayTiepNhanEnd != DateTime.MaxValue && NgayTiepNhanBegin != NgayTiepNhanEnd)
        {
            
            filteredResults = [.. filteredResults.Where(d => d.NgayTiepNhan >= NgayTiepNhanBegin
                                                  && d.NgayTiepNhan <= NgayTiepNhanEnd)];
        }
        Debug.WriteLine(filteredResults.Count);

        // Tìm theo Nợ đại lý
        if (NoDaiLyBegin != 0 && NoDaiLyEnd != long.MaxValue)
        {
            Debug.WriteLine("Go NoDaiLy");
            Debug.WriteLine(NoDaiLyBegin);
            Debug.WriteLine(NoDaiLyEnd);
            filteredResults = [.. filteredResults.Where(d => d.NoDaiLy >= NoDaiLyBegin && d.NoDaiLy <= NoDaiLyEnd)];
        }

        // Tìm theo Nợ tối đa
        if (NoToiDaBegin != 0 && NoToiDaEnd != int.MaxValue)
        {
            filteredResults = [.. filteredResults.Where(d => d.LoaiDaiLy.NoToiDa >= NoToiDaBegin
                                                  && d.LoaiDaiLy.NoToiDa <= NoToiDaEnd)];
        }
        Debug.WriteLine(filteredResults.Count);

        // Tìm theo Mã phiếu xuất
        if (MaPhieuXuatBegin != 0 && MaPhieuXuatEnd != int.MaxValue)
        {


            filteredResults = [.. filteredResults.Where(d => d.PhieuXuats.Any(p => p.MaPhieuXuat >= MaPhieuXuatBegin
                                                                       && p.MaPhieuXuat <= MaPhieuXuatEnd))];
        }

        //// Tìm theo Ngày lập phiếu xuất
        if (NgayLapPhieuXuatBegin != DateTime.MinValue && NgayLapPhieuXuatEnd != DateTime.MinValue && NgayLapPhieuXuatBegin != NgayLapPhieuXuatEnd)
        {

            filteredResults = [.. filteredResults.Where(d => d.PhieuXuats.Any(p => p.NgayLap >= NgayLapPhieuXuatBegin && p.NgayLap <= NgayLapPhieuXuatEnd))];
        }

        // Tìm theo Tổng giá trị phiếu xuất
        if (TongGiaTriPXBegin != 0 && TongGiaTriPXEnd != long.MaxValue)
        {
            filteredResults = [.. filteredResults.Where(d => d.PhieuXuats.Any(p => p.TongGiaTri >= TongGiaTriPXBegin
                                                                       && p.TongGiaTri <= TongGiaTriPXEnd))];
        }
        
        // Tìm theo Mặt hàng
        if (selectedMatHang != null)
        {
            Debug.WriteLine(filteredResults.Count);
            Debug.WriteLine(selectedMatHang.MaMatHang);
            filteredResults = [.. filteredResults.Where(d => d.PhieuXuats.Any(p => p.CTPhieuXuats
            .Any(ct => ct.MatHang.MaMatHang == selectedMatHang.MaMatHang)))];
            foreach (var item in filteredResults)
            {
                foreach ( var px in item.PhieuXuats)
                {
                    if(px.CTPhieuXuats.Count == 0 )
                        Debug.WriteLine($"Failed");
                    foreach (var ctpx in px.CTPhieuXuats)
                    {

                        if (ctpx.MaMatHang == selectedMatHang.MaMatHang)
                            Debug.WriteLine($"DaiLy co ma:{item.MaDaiLy} ");
                        else
                            Debug.WriteLine($"Failed");
                    }
                }
            }

        }

        // Tìm theo Số lượng xuất
        if (SoLuongXuatBegin != 0 || SoLuongXuatEnd != int.MaxValue)
        {
            filteredResults = [.. filteredResults.Where(d => d.PhieuXuats.Count >= SoLuongXuatBegin
                                                  && d.PhieuXuats.Count <= SoLuongXuatEnd)];
        }

        // Tìm theo Đơn giá xuất
        if (DonGiaXuatBegin != 0 || DonGiaXuatEnd != long.MaxValue)
        {
            filteredResults = [.. filteredResults.Where(d => d.PhieuXuats.Any(p => p.CTPhieuXuats
        .Any(ct => ct.DonGiaXuat >= DonGiaXuatBegin && ct.DonGiaXuat <= DonGiaXuatEnd)))];
        }

        // Tìm theo Thành tiền
        if (ThanhTienBegin != 0 || ThanhTienEnd != long.MaxValue)
        {
            filteredResults = [.. filteredResults.Where(d => d.PhieuXuats.Any(p => p.CTPhieuXuats
        .Any(ct => ct.ThanhTien >= ThanhTienBegin && ct.ThanhTien <= ThanhTienEnd)))];
        }

        // Tìm theo Số lượng tồn
        if (SoLuongTonBegin != 0 || soLuongTonEnd != int.MaxValue)
        {
            filteredResults = [.. filteredResults.Where(d => d.PhieuXuats.Any(p => p.CTPhieuXuats
        .Any(ct => ct.MatHang.SoLuongTon >= SoLuongTonBegin && ct.MatHang.SoLuongTon <= soLuongTonEnd)))];
        }

        // Tìm theo Loại đại lý
        if (selectedLoaiDaiLy != null)
        {
            filteredResults = [.. filteredResults.Where(d => d.LoaiDaiLy.TenLoaiDaiLy == selectedLoaiDaiLy.TenLoaiDaiLy)];
        }

        // Tìm theo Quận
        if (selectedQuans != null)
        {
            filteredResults = [.. filteredResults.Where(d => d.Quan.TenQuan == selectedQuans.TenQuan)];
        }

        FoundDailies = [.. filteredResults];
        foreach (var item in FoundDailies)
        {
            Debug.WriteLine(item.MaDaiLy);
        }
        Debug.WriteLine(FoundDailies.Count);

    }
    bool hasNumber(int origin, int entered)
    {
        string numStr = origin.ToString();
        string segStr = entered.ToString();

        return numStr.Contains(segStr);
    }
    bool hasWord(string origin, string entered) {
        return origin.Contains(entered);
    }
}

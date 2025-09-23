
using CommunityToolkit.Mvvm.ComponentModel;
using QuanLyDaiLy.Converter;
using QuanLyDaiLy.Repositories;

namespace QuanLyDaiLy.Models.DTO;

public partial class CTPhieuXuatDTO : ObservableObject
{

    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DonViTinh))]
    [NotifyPropertyChangedFor(nameof(SoLuongTon))]
    private MatHang? selectedMatHang;
    [NotifyPropertyChangedFor(nameof(ThanhTien))]
    [ObservableProperty]
    private string soLuongXuat = " ";
    [NotifyPropertyChangedFor(nameof(ThanhTien))]
    [ObservableProperty]
    private string donGia = " ";
    [ObservableProperty]
    private bool isSelected = false;
    [ObservableProperty]
    public int sTT ;
    public string TenMatHang => selectedMatHang?.TenMatHang ?? "";
    public string DonViTinh => selectedMatHang?.DonViTinh.TenDonViTinh?? "";
    public int SoLuongTon => selectedMatHang?.SoLuongTon ?? 0;

    public long ThanhTien
    {
        get
        {
            int soLuong = 0;
            int donGiaValue = 0;
            int.TryParse(SoLuongXuat, out soLuong);
            int.TryParse(DonGia, out donGiaValue);
            return soLuong * donGiaValue;
        }
    }
}

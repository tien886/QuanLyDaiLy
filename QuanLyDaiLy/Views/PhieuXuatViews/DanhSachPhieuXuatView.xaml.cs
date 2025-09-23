using CommunityToolkit.Maui.Views;
using QuanLyDaiLy.ViewModels.DanhSachDaiLyViewModels;
using QuanLyDaiLy.ViewModels.DanhSachPhieuXuatViewModels;

namespace QuanLyDaiLy.Views.DanhSachPhieuXuat;

public partial class DanhSachPhieuXuatView : ContentPage
{
	public DanhSachPhieuXuatView(DanhSachPhieuXuatViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}
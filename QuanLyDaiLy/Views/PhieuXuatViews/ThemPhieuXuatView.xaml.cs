using CommunityToolkit.Maui.Views;
using QuanLyDaiLy.ViewModels.DanhSachPhieuXuatViewModels;

namespace QuanLyDaiLy.Views.PhieuXuatViews;

public partial class ThemPhieuXuatView : Popup
{
	public ThemPhieuXuatView(ThemPhieuXuatViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		vm.SetPopupReference(this);
    }
}
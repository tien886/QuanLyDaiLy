using CommunityToolkit.Maui.Views;
using QuanLyDaiLy.ViewModels.DanhSachDaiLyViewModels;

namespace QuanLyDaiLy.Views;

public partial class ThemDaiLyView : Popup
{
	public ThemDaiLyView(ThemDaiLyViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		vm.SetPopupReference(this);
	}
}
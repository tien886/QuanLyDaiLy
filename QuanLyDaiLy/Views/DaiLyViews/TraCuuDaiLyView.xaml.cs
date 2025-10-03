using CommunityToolkit.Maui.Views;
using QuanLyDaiLy.ViewModels.DanhSachDaiLyViewModels;

namespace QuanLyDaiLy.Views.DaiLyViews;

public partial class TraCuuDaiLyView : Popup
{
	public TraCuuDaiLyView(TraCuuDaiLyViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		vm.SetPopupReference(this);
    }
}
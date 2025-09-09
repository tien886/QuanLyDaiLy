using QuanLyDaiLy.ViewModels.DanhSachDaiLyViewModels;

namespace QuanLyDaiLy.Views;

public partial class DanhSachDaiLyView : ContentPage
{
	public DanhSachDaiLyView( DanhSachDaiLyViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}
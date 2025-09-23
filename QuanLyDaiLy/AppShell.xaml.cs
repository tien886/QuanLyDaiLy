using QuanLyDaiLy.Views;
using QuanLyDaiLy.Views.DanhSachPhieuXuat;
using QuanLyDaiLy.Views.PhieuXuatViews;

namespace QuanLyDaiLy
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Application.Current.UserAppTheme = AppTheme.Light;
            Routing.RegisterRoute(nameof(DanhSachDaiLyView), typeof(DanhSachDaiLyView));
            Routing.RegisterRoute(nameof(ThemDaiLyView), typeof(ThemDaiLyView));
            Routing.RegisterRoute(nameof(DanhSachPhieuXuatView), typeof(DanhSachPhieuXuatView));
            Routing.RegisterRoute(nameof(ThemPhieuXuatView), typeof(ThemPhieuXuatView));
        }
    }
}

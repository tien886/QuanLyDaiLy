namespace QuanLyDaiLy
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Application.Current.UserAppTheme = AppTheme.Light;
            Routing.RegisterRoute(nameof(Views.DanhSachDaiLyView), typeof(Views.DanhSachDaiLyView));
            Routing.RegisterRoute(nameof(Views.ThemDaiLyView), typeof(Views.ThemDaiLyView));
        }
    }
}

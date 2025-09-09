using Java.Nio.Channels;
using QuanLyDaiLy.Configs;
using QuanLyDaiLy.Models;
using QuanLyDaiLy.Services;
using System.Diagnostics;

namespace QuanLyDaiLy.ServiceImpls;

public class DatabaseServiceImpl : DatabaseService
{
    private readonly DatabaseConfig databaseConfig;
    public DatabaseServiceImpl(DatabaseConfig databaseConfig)
    {
        this.databaseConfig = databaseConfig;
    }
    public async Task InitializeAsync()
    {
        databaseConfig.Initialize();
        await SeedData();
    }
    public async Task SeedData()
    {
        Debug.WriteLine("BEGIN HERE!");
        var context = databaseConfig.DataContext;
        if (context.Quans.Any() || context.Dailies.Any() || context.LoaiDailies.Any() || context.ThamSos.Any())
            return;
        // seeding Quans
        var quans = new List<Quan>
        {
            new Quan { TenQuan = "Quận 1"},
            new Quan { TenQuan = "Quận 2"},
            new Quan { TenQuan = "Quận 3"},
            new Quan { TenQuan = "Quận 4"},
            new Quan { TenQuan = "Quận 5"},
            new Quan { TenQuan = "Quận 6"},
            new Quan { TenQuan = "Quận 7"},
            new Quan { TenQuan = "Quận 8"},
            new Quan { TenQuan = "Quận 9"},
            new Quan { TenQuan = "Quận 10"},
            new Quan { TenQuan = "Quận 11"},
            new Quan { TenQuan = "Quận 12"},
        };
        context.Quans.AddRange(quans);
        await context.SaveChangesAsync();
        // seeding LoaiDaiLies
        var loaiDailies = new List<LoaiDaiLy>
        {
            new LoaiDaiLy { TenLoaiDaiLy = "Loại I", NoToiDa = 1000000 },
            new LoaiDaiLy { TenLoaiDaiLy = "Loại II", NoToiDa = 2000000 },
        };
        context.LoaiDailies.AddRange(loaiDailies);
        await context.SaveChangesAsync();
        // seeding DaiLies
        var dailies = new List<DaiLy>
        {
            new DaiLy{
                Ten = "Đại lý A",
                DiaChi = "123 Trần Hưng Đạo",
                Email = "a@gmail.com",
                NgayTiepNhan = DateTime.Now.AddDays(-10),
                NoDaiLy = 500000,
                MaQuan = quans[0].MaQuan,
                MaLoaiDaiLy = loaiDailies[0].MaLoaiDaiLy,
                SoDienThoai = "0123456789"
            },
            new DaiLy{
                Ten = "Đại lý B",
                DiaChi = "456 Trần Quốc Tuấn",
                Email = "b@gmail.com",
                NgayTiepNhan = DateTime.Now.AddDays(-5),
                NoDaiLy = 1500000,
                MaQuan = quans[1].MaQuan,
                MaLoaiDaiLy = loaiDailies[1].MaLoaiDaiLy,
                SoDienThoai = "0123456788"
            },
            new DaiLy{
                Ten = "Đại lý C",
                DiaChi = "789 Trần Đại Nghĩa",
                Email = "c@gmail.com",
                NgayTiepNhan = DateTime.Now.AddDays(-7),
                NoDaiLy = 2500000,
                MaQuan = quans[2].MaQuan,
                MaLoaiDaiLy = loaiDailies[1].MaLoaiDaiLy,
                SoDienThoai = "0123456787"
            },
            new DaiLy{
                Ten = "Đại lý D",
                DiaChi = "101 Nguyễn Du",
                Email = "d@gmail.com",
                NgayTiepNhan = DateTime.Now.AddDays(-1),
                NoDaiLy = 3000000,
                MaQuan = quans[3].MaQuan,
                MaLoaiDaiLy = loaiDailies[1].MaLoaiDaiLy,
                SoDienThoai = "0923476760"
            },
            new DaiLy
            {
                Ten = "Đại lý STU",
                DiaChi = "505 Hoàng Văn Thụ",
                Email = "stu@dailystu.com",
                NgayTiepNhan = DateTime.Now.AddDays(-3),
                NoDaiLy = 3800000,
                MaQuan = quans[3].MaQuan,
                MaLoaiDaiLy = loaiDailies[1].MaLoaiDaiLy,
                SoDienThoai = "0829456788"
            },
            new DaiLy
            {
                Ten = "Đại lý VWX",
                DiaChi = "606 Hai Bà Trưng",
                Email = "vwx@dailyvwx.com",
                NgayTiepNhan = DateTime.Now.AddDays(-2),
                NoDaiLy = 5500000,
                MaQuan = quans[0].MaQuan,
                MaLoaiDaiLy = loaiDailies[0].MaLoaiDaiLy,
                SoDienThoai = "0456456788"
            },
            new DaiLy
            {
                Ten = "Đại lý YZ",
                DiaChi = "707 Nam Kỳ Khởi Nghĩa",
                Email = "yz@dailyyz.com",
                NgayTiepNhan = DateTime.Now.AddDays(-1),
                NoDaiLy = 2100000,
                MaQuan = quans[1].MaQuan,
                MaLoaiDaiLy = loaiDailies[1].MaLoaiDaiLy,
                SoDienThoai = "0987456788"
            },
            new DaiLy
            {
                Ten = "Đại lý SO1",
                DiaChi = "707 Nguyễn Thái Học",
                Email = "01@dailys01.com",
                NgayTiepNhan = DateTime.Now.AddDays(-1),
                NoDaiLy = 12000000,
                MaQuan = quans[0].MaQuan,
                MaLoaiDaiLy = loaiDailies[1].MaLoaiDaiLy,
                SoDienThoai = "0987454588"
            },
            new DaiLy
            {
                Ten = "Đại lý SO2",
                DiaChi = "707 Nam Kỳ Khởi Nghĩa",
                Email = "02@dailys02.com",
                NgayTiepNhan = DateTime.Now.AddDays(-1),
                NoDaiLy = 2100000,
                MaQuan = quans[1].MaQuan,
                MaLoaiDaiLy = loaiDailies[1].MaLoaiDaiLy,
                SoDienThoai = "0987412788"
            }
        };
        // seeding ThamSos
        context.Dailies.AddRange(dailies);
        await context.SaveChangesAsync();
        var thamSo = new ThamSo
        {
            TenThamSo = "SoLuongDaiLyToiDa",
            GiaTri = "3"
        };
        context.ThamSos.Add(thamSo);
        await context.SaveChangesAsync();
        //seeding MatHangs
        var matHangs = new List<MatHang>
        {
            new MatHang
            {
                TenMatHang = "Gậy như ý",
                SoLuongTon = 50
            },
            new MatHang
            {
                TenMatHang = "Cần đẩu vân",
                SoLuongTon = 100
            },
            new MatHang
            {
                TenMatHang = "Vòng kim cô",
                SoLuongTon = 30
            },
            new MatHang
            {
                TenMatHang = "Túi thần kì",
                SoLuongTon = 50
            },
            new MatHang
            {
                TenMatHang = "Vòng tròn xuyên thấu",
                SoLuongTon = 70
            },
            new MatHang
            {
                TenMatHang = "Chong chóng tre",
                SoLuongTon = 90
            },
        };
        context.MatHangs.AddRange(matHangs);
        await context.SaveChangesAsync();
        // seeding PhieuXuat
        var phieuXuats = new List<PhieuXuat>
        {
            new PhieuXuat
            {
                MaDaiLy = 1,
                NgayLap = DateTime.Now.AddDays(-1000),
                TongGiaTri = 100000,

            },
            new PhieuXuat
            {
                MaDaiLy = 1,
                NgayLap = DateTime.Now.AddDays(-1000),
                TongGiaTri = 119000,
            },
            new PhieuXuat
            {
                MaDaiLy = 1,
                NgayLap = DateTime.Now.AddDays(-1000),
                TongGiaTri = 200000,
            },
            new PhieuXuat
            {
                MaDaiLy = 1,
                NgayLap = DateTime.Now.AddDays(-1000),
                TongGiaTri = 500000,
            },
            new PhieuXuat
            {
                MaDaiLy = 1,
                NgayLap = DateTime.Now.AddDays(-900),
                TongGiaTri = 600000,
            },
            // PhieuXuat of daily02
            new PhieuXuat
            {
                MaDaiLy = 2,
                NgayLap = DateTime.Now.AddDays(-800),
                TongGiaTri = 300000,
            },
            new PhieuXuat
            {
                MaDaiLy = 2,
                NgayLap = DateTime.Now.AddDays(-800),
                TongGiaTri = 400000,
            },
            new PhieuXuat
            {
                MaDaiLy = 2,
                NgayLap = DateTime.Now.AddDays(-800),
                TongGiaTri = 600000,
            },
            new PhieuXuat
            {
                MaDaiLy = 2,
                NgayLap = DateTime.Now.AddDays(-800),
                TongGiaTri = 100000,
            },
            // PhieuXuat of daily03
            new PhieuXuat
            {
                MaDaiLy = 3,
                NgayLap = DateTime.Now.AddDays(-400),
                TongGiaTri = 300000,
            },
            new PhieuXuat
            {
                MaDaiLy = 3,
                NgayLap = DateTime.Now.AddDays(-400),
                TongGiaTri = 180000,
            },
            new PhieuXuat
            {
                MaDaiLy = 3,
                NgayLap = DateTime.Now.AddDays(-400),
                TongGiaTri = 108000,
            },
            // PhieuXuat of daily04
            new PhieuXuat
            {
                MaDaiLy = 4,
                NgayLap = DateTime.Now.AddDays(-600),
                TongGiaTri = 1000000,
            },
            new PhieuXuat
            {
                MaDaiLy = 4,
                NgayLap = DateTime.Now.AddDays(-600),
                TongGiaTri = 100000,
            },
            new PhieuXuat
            {
                MaDaiLy = 4,
                NgayLap = DateTime.Now.AddDays(-600),
                TongGiaTri = 700000,
            },
            new PhieuXuat
            {
                MaDaiLy = 4,
                NgayLap = DateTime.Now.AddDays(-600),
                TongGiaTri = 600000,
            },
        };
        context.PhieuXuats.AddRange(phieuXuats);
        await context.SaveChangesAsync();
        // seeding CTPhieuXuats
        var ctphieuXuats = new List<CTPhieuXuat>()
        {
            new CTPhieuXuat
            {
                SoLuongXuat = 100,
                DonGiaXuat = 
            }

        };
        Debug.WriteLine("STOP SEEDING HERE!");
    }
}

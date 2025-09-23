using Microsoft.EntityFrameworkCore;
using QuanLyDaiLy.Models;
namespace QuanLyDaiLy.Helper.DataSeedModule;

class CTPhieuXuatSeeder
{
    public static void SeedCTPhieuXuat(ModelBuilder modelBuilder)
    {
        var rnd = new Random();
        var ctPhieuXuat = new List<CTPhieuXuat>();
        int maCTPhieuXuat = 1;
        for (int phieu = 1; phieu <= 100; phieu++)
        {
            int SoMatHang = rnd.Next(1, 3);
            for(int i = 0; i < SoMatHang; i++)
            {
                int maMatHang = rnd.Next(1, 50);
                int soLuongXuat = rnd.Next(1, 300);
                int donGiaXuat = rnd.Next(20000, 3000000);
                int maPhieuXuat = phieu;
                long thanhTien = soLuongXuat * donGiaXuat;
                ctPhieuXuat.Add(new CTPhieuXuat
                {
                    MaCTPhieuXuat = maCTPhieuXuat,
                    MaPhieuXuat = maPhieuXuat,
                    MaMatHang = maMatHang,
                    SoLuongXuat = soLuongXuat,
                    DonGiaXuat = donGiaXuat,
                    ThanhTien = thanhTien
                });
                maCTPhieuXuat++;
            }
        }

        modelBuilder.Entity<CTPhieuXuat>().HasData(ctPhieuXuat);
    }
}

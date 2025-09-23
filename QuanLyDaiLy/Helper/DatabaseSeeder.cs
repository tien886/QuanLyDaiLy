using Microsoft.EntityFrameworkCore;
using QuanLyDaiLy.Helper.DataSeedModule;
using QuanLyDaiLy.Helper.SeedData;
using System;
using System.Diagnostics;
namespace QuanLyDaiLy.Helper;

public partial class DatabaseSeeder
{
    public static void SeedData(ModelBuilder modelBuilder)
    {
        DaiLySeeder.SeedDaiLy(modelBuilder);
        Debug.WriteLine("Seed daily succesfullly");
        LoaiDaiLySeeder.SeedLoaiDaiLy(modelBuilder);
        Debug.WriteLine("Seed loaidaily succesfullly");
        QuanSeeder.SeedQuan(modelBuilder);
        Debug.WriteLine("Seed quan succesfullly");
        ThamSoSeeder.SeedThamSo(modelBuilder);
        Debug.WriteLine("Seed thamso succesfullly");
        PhieuXuatSeeder.SeedPhieuXuat(modelBuilder);
        Debug.WriteLine("Seed phieuxuat succesfullly");
        CTPhieuXuatSeeder.SeedCTPhieuXuat(modelBuilder);
        Debug.WriteLine("Seed ctphieuxuat succesfullly");
        MatHangSeeder.SeedMatHang(modelBuilder);
        Debug.WriteLine("Seed mathang succesfullly");
        DonViTinhSeeder.SeedDonViTinh(modelBuilder);
        Debug.WriteLine("Seed donvitinh succesfullly");
    }
}

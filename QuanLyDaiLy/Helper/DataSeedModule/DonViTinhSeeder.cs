
using Microsoft.EntityFrameworkCore;
using QuanLyDaiLy.Models;
namespace QuanLyDaiLy.Helper.DataSeedModule;

class DonViTinhSeeder
{
    public static void SeedDonViTinh(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DonViTinh>().HasData(
            new DonViTinh { MaDonViTinh = 1, TenDonViTinh = "Kg" },
            new DonViTinh { MaDonViTinh = 2, TenDonViTinh = "Cái" },
            new DonViTinh { MaDonViTinh = 3, TenDonViTinh = "Thùng" },
            new DonViTinh { MaDonViTinh = 4, TenDonViTinh = "Lít" },
            new DonViTinh { MaDonViTinh = 5, TenDonViTinh = "Chai" }
        );
    }
}

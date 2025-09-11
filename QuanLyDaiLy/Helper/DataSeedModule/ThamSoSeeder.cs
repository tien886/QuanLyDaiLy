

using Microsoft.EntityFrameworkCore;
using QuanLyDaiLy.Models;

namespace QuanLyDaiLy.Helper.SeedData;

class ThamSoSeeder 
{
    public static void SeedThamSo(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ThamSo>().HasData(
            new ThamSo { TenThamSo = "SoLuongDaiLyToiDa", GiaTri = "10" }
        );
    }
}

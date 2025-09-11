using Microsoft.EntityFrameworkCore;
using QuanLyDaiLy.Helper.DataSeedModule;
using QuanLyDaiLy.Helper.SeedData;
using System;
namespace QuanLyDaiLy.Helper;

public partial class DatabaseSeeder
{
    public static void SeedData(ModelBuilder modelBuilder)
    {
        DaiLySeeder.SeedDaiLy(modelBuilder);
        LoaiDaiLySeeder.SeedLoaiDaiLy(modelBuilder);
        QuanSeeder.SeedQuan(modelBuilder);
        ThamSoSeeder.SeedThamSo(modelBuilder);  
    }
}

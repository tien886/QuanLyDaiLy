
using Microsoft.EntityFrameworkCore;
using QuanLyDaiLy.Helper;
using QuanLyDaiLy.Models;

namespace QuanLyDaiLy.Data;

public partial class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }
    public DbSet<DaiLy> Dailies { get; set; } = null!;  // A table for Dailies entity
    public DbSet<LoaiDaiLy> LoaiDailies { get; set; } = null!;  // A table for LoaiDaiLy entity
    public DbSet<Quan> Quans { get; set; } = null!; // A table for Quan entity
    public DbSet<ThamSo> ThamSos { get; set; } = null!; // A table for ThamSo entity
    public DbSet<PhieuXuat> PhieuXuats { get; set; } = null!; // A table for PhieuXuat entity
    public DbSet<CTPhieuXuat> CTPhieuXuats { get; set; } = null!; // A table for CTPhieuXuat entity
    public DbSet<MatHang> MatHangs { get; set; } = null!; // A table for MatHang entity
    public DbSet<DonViTinh> DonViTinhs { get; set; } = null!; // A table for DonViTinh entity

    // Let the db comprehend the relationships between the entities 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<LoaiDaiLy>()
            .HasMany(dl => dl.Dailies)
            .WithOne(ldl => ldl.LoaiDaiLy)
            .HasForeignKey(mldl => mldl.MaLoaiDaiLy)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Quan>()
            .HasMany(dl => dl.Dailies)
            .WithOne(q => q.Quan)
            .HasForeignKey(mq => mq.MaQuan)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<DaiLy>()
            .HasMany(px => px.PhieuXuats)
            .WithOne(dl => dl.DaiLy)
            .HasForeignKey(dl => dl.MaDaiLy)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<PhieuXuat>()
            .HasMany(ctpx => ctpx.CTPhieuXuats)
            .WithOne(px => px.PhieuXuat)
            .HasForeignKey(px => px.MaPhieuXuat)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<MatHang>()
            .HasMany(ctpx => ctpx.CTPhieuXuats)
            .WithOne(mh => mh.MatHang)
            .HasForeignKey(mh => mh.MaMatHang)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<DonViTinh>()
            .HasMany(mh => mh.MatHangs)
            .WithOne(dvt => dvt.DonViTinh)
            .HasForeignKey(dvt => dvt.MaDonViTinh)
            .OnDelete(DeleteBehavior.Cascade);
        //if( !(Quans.Any() || Dailies.Any() || LoaiDailies.Any() || ThamSos.Any() || PhieuXuats.Any() || CTPhieuXuats.Any() || DonViTinhs.Any() || MatHangs.Any() ))
            DatabaseSeeder.SeedData(modelBuilder);
    }
}
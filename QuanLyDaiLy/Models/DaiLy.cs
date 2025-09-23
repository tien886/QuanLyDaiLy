
using System.ComponentModel.DataAnnotations;

namespace QuanLyDaiLy.Models;

public class DaiLy
{
    [Key]
    public int MaDaiLy { get; set; }
    public string Ten { get; set; } = "";
    public string DiaChi { get; set; } = "";
    public string Email { get; set; } = ""; 
    public DateTime NgayTiepNhan { get; set; } = DateTime.Now;
    public string SoDienThoai { get; set; } = "";
    public long NoDaiLy { get; set; } = 0;
    // Foreign key
    
    public int MaLoaiDaiLy { get; set; }
    public virtual LoaiDaiLy LoaiDaiLy { get; set; } = null!;
    // Let the db know LoaiDaiLy is PK of LoaiDaiLy table and MaLoaiDaiLy is FK
    public int MaQuan { get; set; }
    public virtual Quan Quan { get; set; } = null!;
    // Let the db know Quan is PK of Quan table and MaQuan is FK
    public virtual List<PhieuXuat> PhieuXuats { get; set; } = [];
}


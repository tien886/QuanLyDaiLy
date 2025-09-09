
using System.ComponentModel.DataAnnotations;

namespace QuanLyDaiLy.Models;
public class CTPhieuXuat
{
    [Key]
    public int MaCTPhieuXuat { get; set; }
    public int SoLuongXuat { get; set; }
    public double DonGiaXuat { get; set; }
    public double ThanhTien { get; set; }

    // Foreign key and Primary key
    public int MaPhieuXuat { get; set; }
    public virtual PhieuXuat PhieuXuat { get; set; } = null!;
    public int MaMathang { get; set; }
    public virtual MatHang MatHang { get; set; } = null!;
}

using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace QuanLyDaiLy.Models;

public class MatHang
{
    [Key]
    public int MaMathang { get; set; }
    public string TenMatHang { get; set; } = "";
    public int SoLuongTon { get; set; } = 0;
    // Foreign key
    public int MaDonViTinh { get; set; }
    public virtual DonViTinh DonViTinh { get; set; } = null!;
    public virtual List<CTPhieuXuat> CTPhieuXuats { get; set; } = [];
}

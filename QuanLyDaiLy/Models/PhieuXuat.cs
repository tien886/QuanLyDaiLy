using System.ComponentModel.DataAnnotations;

namespace QuanLyDaiLy.Models;

public class PhieuXuat
{
    [Key]
    public int MaPhieuXuat { get; set; }
    public DateTime NgayLap { get; set; }
    public long TongGiaTri { get; set; }
    //Foreign key
    public int MaDaiLy { get; set; }
    public virtual DaiLy DaiLy { get; set; } = null!;
    //  
    public virtual List<CTPhieuXuat> CTPhieuXuats { get; set; } = [];
}

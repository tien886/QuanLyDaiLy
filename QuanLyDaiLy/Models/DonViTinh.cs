
using System.ComponentModel.DataAnnotations;

namespace QuanLyDaiLy.Models;

public class DonViTinh
{
    [Key]
    public int MaDonViTinh { get; set; }
    public string TenDonViTinh { get; set; } = "";
    public virtual List<MatHang> MatHangs { get; set; } = [];

}

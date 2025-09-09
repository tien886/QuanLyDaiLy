using System.ComponentModel.DataAnnotations;

namespace QuanLyDaiLy.Models;

public class LoaiDaiLy
{
    [Key]
    public int MaLoaiDaiLy { get; set; } = 0;   // set the PK is MaLoaiDaiLy
    public string TenLoaiDaiLy { get; set; } = "";
    public double NoToiDa { get; set; } = 0;

    // Navigation property
    public virtual List<DaiLy> Dailies { get; set; } = [];
}
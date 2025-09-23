
using QuanLyDaiLy.Models;

namespace QuanLyDaiLy.Services;

public interface IMatHangService
{
    Task<IEnumerable<MatHang>> GetAllMatHangsAsync();
    Task<MatHang> GetMatHangByID(int MaMatHang);
    Task UpdateMatHang(MatHang matHang);
}

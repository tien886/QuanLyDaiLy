
using QuanLyDaiLy.Models;

namespace QuanLyDaiLy.Interfaces;
public interface IMatHangRepository
{
    Task<IEnumerable<MatHang>> GetAllMatHangsAsync();
    Task<MatHang> GetMatHangByID(int MaMatHang);
    Task UpdateMatHang(MatHang matHang);

}


using QuanLyDaiLy.Models;

namespace QuanLyDaiLy.Interfaces;

public interface ICTPhieuXuatRepository
{
    Task AddCTPhieuXuatAsync(CTPhieuXuat ctPhieuXuat);
    Task<IEnumerable<CTPhieuXuat>> GetAllChiTietPhieuXuat();
    Task<int> GetNextAvailableIdAsync();
}


using QuanLyDaiLy.Models;

namespace QuanLyDaiLy.Services;

public interface ICTPhieuXuatService
{
    Task AddCTPhieuXuatAsync(CTPhieuXuat ctPhieuXuat);
    Task<IEnumerable<CTPhieuXuat>> GetAllChiTietPhieuXuat();
    Task<int> GetNextCTPhieuXuatAvailableIdAsync();

}

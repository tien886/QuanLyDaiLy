
using QuanLyDaiLy.Models;

namespace QuanLyDaiLy.Services;

public interface IPhieuXuatService
{
    Task<int> AddPhieuXuatAsync(PhieuXuat newPhieuXuat);
    Task<int> GetNextAvailableIdAsync();
    Task<IEnumerable<PhieuXuat>> GetAllPhieuXuatAsync();
    Task<IEnumerable<PhieuXuat>> GetPhieuXuatsPaging(int pageNumber, int pageSize);
}

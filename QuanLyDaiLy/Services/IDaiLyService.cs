
using QuanLyDaiLy.Models;

namespace QuanLyDaiLy.Services;

public interface IDaiLyService
{
    Task<IEnumerable<DaiLy>> GetAllDaiLyAsync();
    Task<int> AddDaiLyAsync(DaiLy newDaiLy);
    Task<int> GetNextAvailableIdAsync();
    Task<IEnumerable<DaiLy>> GetDailiesPaging(int pageNumber, int pageSize);

}

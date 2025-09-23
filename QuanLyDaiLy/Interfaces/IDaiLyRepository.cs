using QuanLyDaiLy.Models;
using System;

namespace QuanLyDaiLy.Interfaces;

public interface IDaiLyRepository
{
    Task<IEnumerable<DaiLy>> GetAllDaiLyAsync();
    Task<int> AddDaiLyAsync(DaiLy newDaiLy);
    Task<int> GetNextAvailableIdAsync();
    Task<IEnumerable<DaiLy>> GetDailiesPaging(int pageNumber, int pageSize);
}

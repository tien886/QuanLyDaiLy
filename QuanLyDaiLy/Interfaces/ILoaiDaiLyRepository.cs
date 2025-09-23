

using QuanLyDaiLy.Models;

namespace QuanLyDaiLy.Interfaces;

public interface ILoaiDaiLyRepository
{
    Task<IEnumerable<LoaiDaiLy>> GetAllLoaiDaiLyAsync();
    Task<LoaiDaiLy?> GetLoaiDaiLyByIdAsync(int id);
}

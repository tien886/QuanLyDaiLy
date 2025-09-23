using QuanLyDaiLy.Models;

namespace QuanLyDaiLy.Services;

public interface ILoaiDaiLyService
{
    Task<IEnumerable<LoaiDaiLy>> GetAllLoaiDaiLyAsync();
    Task<LoaiDaiLy?> GetLoaiDaiLyByIdAsync(int id);

}

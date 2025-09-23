
using Microsoft.EntityFrameworkCore;
using QuanLyDaiLy.Data;
using QuanLyDaiLy.Interfaces;
using QuanLyDaiLy.Models;

namespace QuanLyDaiLy.Repositories;

public class PhieuXuatRepository : IPhieuXuatRepository
{
    private readonly DataContext _dataContext;
    public PhieuXuatRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task<int> AddPhieuXuatAsync(PhieuXuat newPhieuXuat)
    {
        await _dataContext.PhieuXuats.AddAsync(newPhieuXuat);
        return await _dataContext.SaveChangesAsync();
    }
    public async Task<int> GetNextAvailableIDAsync()
    {
        var maxID = await _dataContext.PhieuXuats.MaxAsync(px => (int?)px.MaPhieuXuat) ?? 0;
        return maxID + 1;
    }
    public async Task<IEnumerable<PhieuXuat>> GetAllPhieuXuatAsync()
    {
        return await _dataContext.PhieuXuats.ToListAsync();
    }
    public async Task<IEnumerable<PhieuXuat>> GetPhieuXuatsPaging(int pageNumber, int pageSize)
    {
        return await _dataContext.PhieuXuats
                    .OrderBy(px => px.MaPhieuXuat)
                    .Skip((pageNumber - 1)* pageSize)
                    .Take(pageSize)
                    .ToListAsync();
    }

}

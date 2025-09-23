using Microsoft.EntityFrameworkCore;
using QuanLyDaiLy.Data;
using QuanLyDaiLy.Interfaces;
using QuanLyDaiLy.Models;

namespace QuanLyDaiLy.Repositories;

public class CTPhieuXuatRepository : ICTPhieuXuatRepository
{
    private readonly DataContext _dataContext;
    public CTPhieuXuatRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task AddCTPhieuXuatAsync(CTPhieuXuat ctPhieuXuat)
    {
        await _dataContext.CTPhieuXuats.AddAsync(ctPhieuXuat);
        await _dataContext.SaveChangesAsync();
    }
    public async Task<IEnumerable<CTPhieuXuat>> GetAllChiTietPhieuXuat()
    {
        return await _dataContext.CTPhieuXuats
            .Include(c => c.PhieuXuat)
            .Include(c => c.MatHang)
                .ThenInclude(m => m.DonViTinh)
            .ToListAsync();
    }
    public async Task<int> GetNextAvailableIdAsync()
    {
        var maxID = await _dataContext.CTPhieuXuats.MaxAsync(dl => (int?)dl.MaCTPhieuXuat) ?? 0;
        return maxID + 1;
    }

}

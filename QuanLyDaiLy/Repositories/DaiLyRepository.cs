
using Microsoft.EntityFrameworkCore;
using QuanLyDaiLy.Data;
using QuanLyDaiLy.Interfaces;
using QuanLyDaiLy.Models;

namespace QuanLyDaiLy.Repositories;

public class DaiLyRepository : IDaiLyRepository
{
    private readonly DataContext _dataContext;
    
    public DaiLyRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<int> AddDaiLyAsync(DaiLy newDaiLy)
    {
        await _dataContext.Dailies.AddAsync(newDaiLy);
        return await _dataContext.SaveChangesAsync();   // save changes to db
    }
    
    public async Task<IEnumerable<DaiLy>> GetAllDaiLyAsync()
    {
        return await _dataContext.Dailies
            .Include(dl => dl.Quan)
            .Include(dl => dl.LoaiDaiLy)
            .ToListAsync();
    }
    public async Task<int> GetNextAvailableIdAsync()
    {
        var maxID = await _dataContext.Dailies.MaxAsync(dl => (int?)dl.MaDaiLy) ?? 0;
        return maxID + 1;
    }
}
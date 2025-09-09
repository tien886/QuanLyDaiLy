

using Microsoft.EntityFrameworkCore;
using QuanLyDaiLy.Data;
using QuanLyDaiLy.Interfaces;
using QuanLyDaiLy.Models;
using System.Runtime.CompilerServices;

namespace QuanLyDaiLy.Repositories;

public class LoaiDaiLyRepository : ILoaiDaiLyRepository
{
    private readonly DataContext _dataContext;
    public LoaiDaiLyRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task<IEnumerable<LoaiDaiLy>> GetAllLoaiDaiLyAsync()
    {
        return await _dataContext.LoaiDailies.ToListAsync();
    }
    
}

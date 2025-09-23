
using Microsoft.EntityFrameworkCore;
using QuanLyDaiLy.Data;
using QuanLyDaiLy.Interfaces;
using QuanLyDaiLy.Models;

namespace QuanLyDaiLy.Repositories;

public class MatHangRepository  : IMatHangRepository
{
    private readonly DataContext _dataContext;
    public MatHangRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task<IEnumerable<MatHang>> GetAllMatHangsAsync()
    {
        return await _dataContext.MatHangs.Include(m => m.DonViTinh).ToListAsync();
    }
    public async Task<MatHang> GetMatHangByID(int MaMatHang)
    {
        return await _dataContext.MatHangs.Include(m => m.DonViTinh).FirstOrDefaultAsync(m => m.MaMatHang == MaMatHang);
    }
    public async Task UpdateMatHang(MatHang matHang)
    {
        _dataContext.MatHangs.Update(matHang);
        await _dataContext.SaveChangesAsync();
    }
}

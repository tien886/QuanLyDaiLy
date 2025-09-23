
using QuanLyDaiLy.Data;
using QuanLyDaiLy.Models;

namespace QuanLyDaiLy.Helper;

public class DanhSachDaiLyPartitioner
{
    private readonly DataContext _dataContext = null!;
    public DanhSachDaiLyPartitioner(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public List<DaiLy> GetDaiLies (int pageNumber, int pageSize)
    {
        return _dataContext.Dailies
            .OrderBy(dl => dl.MaDaiLy)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }
    public int GetTotalPages(int pageSize)
    {
        int total = _dataContext.Dailies.Count();
        return (int)Math.Ceiling((double)total / pageSize);
    }
}

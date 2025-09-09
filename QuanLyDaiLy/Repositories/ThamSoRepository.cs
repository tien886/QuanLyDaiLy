
using QuanLyDaiLy.Data;
using QuanLyDaiLy.Interfaces;

namespace QuanLyDaiLy.Repositories;

public class ThamSoRepository : IThamSoRepository
{
    private readonly DataContext _dataContext;
    public ThamSoRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task<string> GetThamSo(string key)
    {
        return _dataContext.ThamSos
            .Where(ts => ts.TenThamSo == key)
            .Select(ts => ts.GiaTri)
            .FirstOrDefault()!;
    }
}

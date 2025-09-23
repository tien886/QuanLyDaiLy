

using QuanLyDaiLy.Data;
using QuanLyDaiLy.Interfaces;

namespace QuanLyDaiLy.Repositories;

public class DonViTinhRepository : IDonViTinhRepository
{
    private readonly DataContext _dataContext;
    public DonViTinhRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task<IEnumerable<string>> GetAllDonViTinhsAsync()
    {
        return await Task.FromResult(_dataContext.DonViTinhs.Select(dvt => dvt.TenDonViTinh).ToList());
    }
}
